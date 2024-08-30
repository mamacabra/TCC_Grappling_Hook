using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkTrap: MonoBehaviour
{
    public List<Transform> waypoints;  // List of waypoints to move to
    public float moveSpeed = 5f;        // Speed of movement
    public float pauseTime = 1f;        // Time to pause at each waypoint

    private Transform targetWaypoint;
    private int currentWaypointIndex = -1;
    [SerializeField] private float timeUntilBite = 5f; 
    [SerializeField] private bool startTimer = false;

    [SerializeField] private GameObject fork;

    void Start()
    {
        fork.SetActive(false);
        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned!");
            return;
        }
        ChooseNextWaypoint();
        
    }

    void Update()
    {
        if (targetWaypoint != null)
        {
            MoveTowardsTarget();
        }

    }

    void MoveTowardsTarget()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            StartCoroutine(PauseAndChooseNextWaypoint());
        }
    }

    void ChooseNextWaypoint()
    {
        if (waypoints.Count == 0)
            return;

        currentWaypointIndex = Random.Range(0, waypoints.Count);
        targetWaypoint = waypoints[currentWaypointIndex];
    }
    private void Bite()
    {

        if(timeUntilBite <= 0)
        {
            //fork comes down

        }
    }

    IEnumerator PauseAndChooseNextWaypoint()
    {
        yield return new WaitForSeconds(pauseTime);
        ChooseNextWaypoint();
    }
}
