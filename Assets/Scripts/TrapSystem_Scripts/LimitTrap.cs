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
        [SerializeField] private List<GameObject> cutlery;

        void Start()
        {
            GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Character");
            foreach (GameObject playerObject in playerObjects)
            {
                Transform playerTransform = playerObject.transform;
                players.Add(new PlayerTrapData
                {
                    character = playerObject.GetComponent<Character.Character>(),
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
                Character.Character playerCharacter = players[i].character;
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
                        if (playerCharacter.CharacterEntity.CharacterState.State is not DeathState)
                        {
                            SpawnFallingObject(player);
                            playerData.isOutsideArena = false;
                            playerData.outsideTime = 0f; 
                        } 
                    }
                }
                else
                {
                    if (playerData.isOutsideArena)
                    {
                        playerData.isOutsideArena = false;
                        playerData.outsideTime = 0f; 
                    }
                }
            }
        }


        void SpawnFallingObject(Transform player)
        {
            if (cutlery.Count != 0)
            {
                var index = Random.Range(0, cutlery.Count);
                fallingObjectPrefab = cutlery[index];
            }; 
            if (fallingObjectPrefab == null)
            {
                Debug.LogError("Prefab do Objeto que Cai não foi atribuído.");
                return;
            }
            
            var randomYRotation = Random.Range(0f, 360f); 
            var randomRotation = Quaternion.Euler(0, randomYRotation, 0);
            
            Vector3 spawnPosition = new Vector3(player.position.x, fallHeight, player.position.z);
            Instantiate(fallingObjectPrefab, spawnPosition, randomRotation);
        }
    }

   

    public class PlayerTrapData
    {
        public Transform playerTransform;
        public Character.Character character;
        public bool isOutsideArena;
        public float outsideTime;
    }
}
