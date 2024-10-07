using System.Collections.Generic;
using Character;
using UnityEngine;
using Character.States;
using Character.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace TrapSystem_Scripts
{
    public class LimitTrap : MonoBehaviour
    {
        [Header("Configurações do Objeto que Cai")]
        public GameObject fallingObjectPrefab;  
        public float fallHeight = 50f;         

        [Header("Limites da Arena")]
        public float minX = -25f;              
        public float maxX = 25f;               
        public float minZ = -25f;              
        public float maxZ = 25f;               

        [Header("Configurações de Tempo")]
        public float timeBeforeDeath = 3f;     

        private List<PlayerTrapData> players = new List<PlayerTrapData>();

        void Start()
        {
            GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Character");
            foreach (GameObject playerObject in playerObjects)
            {
                Transform playerTransform = playerObject.transform;
                players.Add(new PlayerTrapData
                {
                    playerTransform = playerTransform,
                    isOutsideArena = false,
                    outsideTime = 0f
                });
            }

            if (players.Count == 0)
            {
                Debug.Log("Nenhum jogador encontrado! Certifique-se de que os jogadores possuem a tag 'Character'.");
            }
        }

        void Update()
        {
            for (int i = players.Count - 1; i >= 0; i--)
            {
                PlayerTrapData playerData = players[i];
                Transform player = playerData.playerTransform;

                if (player == null)
                {
                    players.RemoveAt(i); 
                    continue;
                }

                bool isPlayerOutside = player.position.x < minX || player.position.x > maxX || 
                                       player.position.z < minZ || player.position.z > maxZ;

                // If the player is outside the arena
                if (isPlayerOutside)
                {
                    // If they just went outside, start the timer
                    if (!playerData.isOutsideArena)
                    {
                        playerData.isOutsideArena = true;
                        playerData.outsideTime = Time.time;
                    }
                    // If they've been outside long enough, trigger the trap
                    else if (Time.time - playerData.outsideTime >= timeBeforeDeath)
                    {
                        SpawnFallingObject(player);
                        // Here we do not remove the player, but reset their status so they can trigger the trap again
                        playerData.isOutsideArena = false; // Reset the state to allow the trap to activate again
                        playerData.outsideTime = 0f; // Reset the time for next exit event
                    }
                }
                // If the player re-enters the arena
                else
                {
                    // Reset their "outside" status and timer to prepare for the next exit
                    if (playerData.isOutsideArena)
                    {
                        playerData.isOutsideArena = false;
                        playerData.outsideTime = 0f; // Reset the outside timer
                    }
                }
            }
        }


        void SpawnFallingObject(Transform player)
        {
            if (fallingObjectPrefab == null)
            {
                Debug.LogError("Prefab do Objeto que Cai não foi atribuído.");
                return;
            }

            Vector3 spawnPosition = new Vector3(player.position.x, fallHeight, player.position.z);
            Instantiate(fallingObjectPrefab, spawnPosition, Quaternion.identity);
        }
    }

   

    public class PlayerTrapData
    {
        public Transform playerTransform;
        public bool isOutsideArena;
        public float outsideTime;
    }
}
