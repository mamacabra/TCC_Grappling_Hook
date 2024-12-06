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
        public GameObject instaKillPrefab;
        public float fallHeight = 50f;         

        [Header("Limites da Arena")]
        public float minX = -25f;              
        public float maxX = 25f;               
        public float minZ = -25f;              
        public float maxZ = 25f;
        private readonly float overLimits = 10f;

        [Header("Configurações de Tempo")]
        public float timeBeforeDeath = 0.5f;     

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

        var isPlayerOutside = player.position.x < minX || player.position.x > maxX || 
                              player.position.z < minZ || player.position.z > maxZ;
        

        
        if (isPlayerOutside)
        {
            if (playerCharacter.CharacterEntity.CharacterState.State is DeathState or WinnerState)return;
            if (!playerData.isOutsideArena)
            {
                playerData.isOutsideArena = true;
                playerData.outsideTime = Time.time;
            }
            else if (Time.time - playerData.outsideTime >= timeBeforeDeath)
            {
                if (playerCharacter.CharacterEntity.CharacterState.State is DeathState or WinnerState)return;
                SpawnFallingObject(player);
                playerData.isOutsideArena = false;
                playerData.outsideTime = 0f;
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
    
            // Time for the object to fall
            float fallTime = Mathf.Sqrt(2 * fallHeight / Physics.gravity.magnitude);

            // Predict future position
            Vector3 playerVelocity = GetPlayerVelocity(player);
            Vector3 predictedPosition = player.position + (playerVelocity * fallTime);

            // Spawn the object at the predicted position
            Vector3 spawnPosition = new Vector3(predictedPosition.x, fallHeight, predictedPosition.z);
            Instantiate(fallingObjectPrefab, spawnPosition, randomRotation);
        }
        Vector3 GetPlayerVelocity(Transform player)
        {
            // Example velocity calculation if Rigidbody is not available
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                return playerRb.velocity;
            }

            // If Rigidbody is not available, calculate velocity manually
            PlayerTrapData playerData = FindPlayerData(player); // Implement this method to get PlayerTrapData
            Vector3 lastPosition = playerData.playerTransform.position;
            playerData.playerTransform.position = player.position;

            return (player.position - lastPosition) / Time.deltaTime;
        }
        
        PlayerTrapData FindPlayerData(Transform player)
        {
            return players.Find(p => p.playerTransform == player);
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
