using System.Collections;
using System.Collections.Generic;
using Character.States;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace TrapSystem_Scripts
{
    public class ForkTrap : MonoBehaviour
    {
        public List<Transform> waypoints;
        public float moveSpeed = 5f;
        public float moveTowardsPlayerSpeed = 1f;
        public float pauseTime = 2f;
        public float delayTime = 3f;  // Adjustable delay before trap starts working
        public float stopTime = 0f;   // Adjustable stop time when colliding with a player

        [SerializeField] private float cooldown = 0f;
        [SerializeField] private bool startCooldown = false;
        [SerializeField] private bool hasBitten = false;
        [SerializeField] private bool isChasing = false;
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
                if (cooldown < 0) cooldown = 0f;
            }

            if ((cooldown <= 0) && hasBitten)
            {
                cooldown = 1f;
                hasBitten = false;
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
            
        }

        

        private void MoveTowardsTarget()
        {
            if (cutlery.Count != 0)
            {
                var index = Random.Range(0, cutlery.Count);
                fork = cutlery[index];
            }; 
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
            var randomYRotation = Random.Range(0f, 360f); 
            var randomRotation = Quaternion.Euler(0, randomYRotation, 0); 
    
            Instantiate(fork, playerPosition + new Vector3(0, alturaGarfo, 0), randomRotation);
            hasBitten = true;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!isTrapActive) return;  

            if (other.gameObject.CompareTag("Character"))
            {
                
                if(other.gameObject.GetComponent<Character.Character>().CharacterEntity.CharacterState.State is DeathState) return;
                bittingPause = true;
                startCooldown = true;
                playerPos = other.gameObject.transform.position;
                isChasing = true;
                if (cooldown <= 0)
                {
                    
                    StartCoroutine(StopTrapAndBite(other.transform.position));
                }
            }
        }
        private void FixedUpdate()
        {
            if (isChasing)
            {
                FollowPlayer(playerPos);
            }
        }

        private void FollowPlayer(Vector3 playerPosition)
        {
            if (timeChasing > 0)
            {
                transform.position = Vector3.Lerp(transform.position, playerPosition, moveTowardsPlayerSpeed * Time.deltaTime); 
                timeChasing -= Time.deltaTime;
            }
            else
            {
                isChasing = false;
                timeChasing = 1f;
            }
        }
        

        private IEnumerator StopTrapAndBite(Vector3 playerPosition)
        {
            isStopped = true;  
            
            yield return new WaitForSeconds(stopTime);  
            
            Bite(playerPosition);
            isStopped = false; 
        }

        private void OnTriggerExit(Collider other)
        {
            if (!isTrapActive) return; 

            if (other.gameObject.CompareTag("Character"))
            {
                bittingPause = false;
            }
        }
    }
}


