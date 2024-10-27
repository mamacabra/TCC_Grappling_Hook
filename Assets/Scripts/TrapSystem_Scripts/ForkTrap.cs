using System.Collections;
using System.Collections.Generic;
using Character.States;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TrapSystem_Scripts
{
    public class ForkTrap : MonoBehaviour
    {
        public List<Transform> waypoints;
        public float moveSpeed = 5f;
        public float pauseTime = 2f;
        public float delayTime = 3f;  // Adjustable delay before trap starts working
        public float stopTime = 2f;   // Adjustable stop time when colliding with a player

        [SerializeField] private float cooldown = 0f;
        [SerializeField] private bool startCooldown = false;
        [SerializeField] private bool hasBitten = false;
        private Transform targetWaypoint;
        private int currentWaypointIndex = -1;
        [SerializeField] private bool bittingPause = false;

        [SerializeField] private GameObject fork;
        [SerializeField] private float alturaGarfo = 0f;

        private bool isTrapActive = false; // Control if the trap is currently active
        private bool isStopped = false;    // Control if the trap is temporarily stopped

        void Start()
        {
            if (waypoints.Count == 0)
            {
                Debug.LogError("No waypoints assigned!");
                return;
            }

            // Start the coroutine to activate the trap after a delay
            StartCoroutine(ActivateTrapAfterDelay());

            ChooseNextWaypoint();
        }

        private IEnumerator ActivateTrapAfterDelay()
        {
            yield return new WaitForSeconds(delayTime); // Wait for the delay time
            isTrapActive = true;  // Activate the trap
        }

        private void Update()
        {
            if (!isTrapActive || isStopped) return; // Don't move or activate if the trap hasn't started or is stopped

            if (targetWaypoint is not null)
            {
                MoveTowardsTarget();
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
            Instantiate(fork, playerPosition + new Vector3(0, alturaGarfo, 0), Quaternion.identity);
            hasBitten = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isTrapActive) return;  // Prevent biting if the trap hasn't activated yet

            if (other.gameObject.CompareTag("Character"))
            {
                if(other.gameObject.GetComponent<Character.Character>().CharacterEntity.CharacterState.State is DeathState) return;
                bittingPause = true;
                startCooldown = true;

                // Stop the trap and start a coroutine to wait before biting
                if (cooldown <= 0)
                {
                    StartCoroutine(StopTrapAndBite(other.transform.position));
                }
            }
        }

        private IEnumerator StopTrapAndBite(Vector3 playerPosition)
        {
            isStopped = true;  // Stop the trap

            yield return new WaitForSeconds(stopTime);  // Wait for the stop time

            // Once the stop time has passed, bite the player and resume the trap movement
            Bite(playerPosition);
            isStopped = false; // Resume the trap movement
        }

        private void OnTriggerExit(Collider other)
        {
            if (!isTrapActive) return;  // Ignore trigger exit if the trap isn't active yet

            if (other.gameObject.CompareTag("Character"))
            {
                bittingPause = false;
            }
        }
    }
}


