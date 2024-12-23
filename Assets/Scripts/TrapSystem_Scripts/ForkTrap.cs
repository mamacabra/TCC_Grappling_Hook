using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character.States;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using LocalMultiplayer;

namespace TrapSystem_Scripts
{
    public class ForkTrap : MonoBehaviour
    {
        public List<Transform> waypoints;
        public float moveSpeed = 5f;
        public float moveTowardsPlayerSpeed = 1f;
        public float pauseTime = 2f;
        public float delayTime = 10f;  // Adjustable delay before trap starts working
        public float stopTime = 0f;   // Adjustable stop time when colliding with a player

        [SerializeField] private float cooldown = 0f;
        [SerializeField] private bool startCooldown = false;
        [SerializeField] private bool hasBitten = false;
        [SerializeField] private bool isChasing = false;
        private bool triggerOn = false;
        private Transform targetWaypoint;
        private int currentWaypointIndex = -1;
        [SerializeField] private bool bittingPause = false;

        [SerializeField] private GameObject fork;
        [SerializeField] private float alturaGarfo = 0f;

        private bool isTrapActive = false; // Control if the trap is currently active
        private bool isStopped = false;
        private Vector3 playerPos;
        private float timeChasing = 1f;
        [SerializeField] private List<GameObject> cutlery;

        //Shadow settings
        public GameObject shadow;
        public Vector3 growthRate = new Vector3(0.1f, 0f, 0.1f); // Growth per second for X and Z
        public Vector3 maxScale = new Vector3(5f, 1f, 5f); // Maximum scale limits
        private List<GameObject> playerObjects = new List<GameObject>();
        private bool deadPlayer = false;
        private List<GameObject> forks = new List<GameObject>();
        
        public TrapSystemManager trapSystemManager;
        

        void Start()
        {

            if (waypoints.Count == 0)
            {
                Debug.LogError("No waypoints assigned!");
                return;
            }
            
            StartCoroutine(ActivateTrapAfterDelay());

            ChooseNextWaypoint();
        }

        private IEnumerator ActivateTrapAfterDelay()
        {
            yield return new WaitForSeconds(delayTime); 
            isTrapActive = true;  
        }

        private void Update()
        {
            if (PlayersManager.Instance.GameOver)
            {
                Destroy(gameObject);
            }
            if(InterfaceManager.Instance.pause) return;
            if(InterfaceManager.Instance.isOnCount) return;
            if (!isTrapActive || isStopped) return; 

            if (targetWaypoint is not null)
            {
                if (!isChasing)
                {
                    MoveTowardsTarget();
                }
            }

            if (startCooldown)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    cooldown = 0f;
                }
            }

            if ((cooldown <= 0) && hasBitten)
            {
                cooldown = 1f;
                if (!triggerOn)
                {
                    hasBitten = false;
                }
            }

            if (bittingPause)
            {
                pauseTime -= Time.deltaTime;
            }

            if (pauseTime < 0) pauseTime = 0f;

            if (pauseTime <= 0)
            {
                bittingPause = false;
                pauseTime = 2f;
            }
            if (isChasing)
            {
                if (playerObjects.Count != 0)
                {
                    playerPos = playerObjects[0].transform.position;
                    FollowPlayer(playerPos);
                }
            }
            if (playerObjects.Count > 0)
            {
                    if (cooldown <= 0)
                    {
                        if (!isChasing)
                        {
                            playerPos = playerObjects[0].transform.position;
                            StartCoroutine(StopTrapAndBite(playerPos));
                            startCooldown = true;
                        }
                    }
            }
            

            var currentScale = transform.localScale;
            
            currentScale.x += growthRate.x * Time.deltaTime;
            currentScale.z += growthRate.z * Time.deltaTime;
            
            currentScale.x = Mathf.Min(currentScale.x, maxScale.x);
            currentScale.z = Mathf.Min(currentScale.z, maxScale.z);
            
            transform.localScale = currentScale;
        }

        private void PlayerGotKilled(GameObject playerKilled)
        {
            playerObjects.Remove(playerKilled);
        }

        

        private void MoveTowardsTarget()
        {
           
            var step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);

            if (!(Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)) return;
            if (!bittingPause)
            {
                ChooseNextWaypoint();
            }
        } 
        

        private void ChooseNextWaypoint()
        {
            if (waypoints.Count == 0) return;

            currentWaypointIndex = Random.Range(0, waypoints.Count);
            targetWaypoint = waypoints[currentWaypointIndex];
        }

        private void Bite(Vector3 playerPosition)
        {
            if (cutlery.Count != 0)
            {
                var index = Random.Range(0, cutlery.Count);
                fork = cutlery[index];
            }; 
            var randomYRotation = Random.Range(0f, 360f); 
            var randomRotation = Quaternion.Euler(0, randomYRotation, 0); 
            var forkfall = Instantiate(fork, playerPosition + new Vector3(0, alturaGarfo, 0), randomRotation).GetComponent<ForkFall>();
            forkfall.OnPlayerKilled.AddListener(PlayerGotKilled);
            hasBitten = true;
        }



        private void OnTriggerEnter(Collider other)
        {
            if (!isTrapActive) return;  
            if (other.gameObject.CompareTag("Character"))
            {
                isChasing = true;
                if(other.gameObject.GetComponent<Character.Character>().CharacterEntity.CharacterState.State is DeathState) return;
                playerObjects.Add(other.gameObject);
                
                if(other.gameObject.GetComponent<Character.Character>().CharacterEntity.CharacterState.State is DeathState) playerObjects.Remove(other.gameObject);
                
            }
        }


        private void FollowPlayer(Vector3 playerPosition)
        {
            if (!(timeChasing > 0)) return;
            transform.position = Vector3.Lerp(transform.position, playerPosition, moveTowardsPlayerSpeed * Time.deltaTime); 
            timeChasing -= Time.deltaTime;
            if (!(timeChasing <= 0)) return;
            isChasing  = false;
            timeChasing = 1f;
        }

        private IEnumerator StopTrapAndBite(Vector3 playerPosition)
        {
            isStopped = true;  
            
            yield return new WaitForSeconds(stopTime);  
            Bite(playerPosition);
            yield return new WaitForSeconds(0.5f);
            isStopped = false;
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (!isTrapActive) return; 

            if (other.gameObject.CompareTag("Character"))
            {
                
                playerObjects.Remove(other.gameObject);
                Debug.Log("Removed player");
                Debug.Log(playerObjects.Count() + " players left");
                bittingPause = false;
                triggerOn = false;
                if (other.gameObject.GetComponent<Character.Character>().CharacterEntity.CharacterState
                        .State is DeathState)
                {
                    playerObjects.Remove(other.gameObject);
                    Debug.Log("Removed player");
                }
            }
            ChooseNextWaypoint();
        }
    }
}


