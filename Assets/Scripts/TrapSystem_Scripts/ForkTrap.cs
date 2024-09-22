using System;
using System.Collections;
using System.Collections.Generic;
using Character.Utils;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ForkTrap: MonoBehaviour
{
    public List<Transform> waypoints; 
    public float moveSpeed = 5f;       
    public float pauseTime = 2f;    
    
    [SerializeField]private float cooldown = 0f;
    [SerializeField]private bool startCooldown = false;
    [SerializeField]private bool hasBitten = false;
    private Transform targetWaypoint;
    private int currentWaypointIndex = -1;
    [SerializeField]private bool bittingPause = false;

    [SerializeField] private GameObject fork;
    [SerializeField] private float alturaGarfo = 0f;

    void Start()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned!");
            return;
        }
        ChooseNextWaypoint();
        
    }
  

    private void Update()
    {
        if (targetWaypoint is not null)
        {
            MoveTowardsTarget();
        }

        if (startCooldown)
        {
            
            cooldown-=Time.deltaTime;
            if(cooldown<0) cooldown = 0f;
        }

        if ((cooldown <= 0) && hasBitten)
        {
            //startCooldown = false;
            cooldown = 1f;
            hasBitten = false;
        }

        if (bittingPause)
        {
            pauseTime-=Time.deltaTime;
        }
        
        if(pauseTime<0) pauseTime = 0f;
        
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
        if (waypoints.Count == 0)
            return;

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
        
        
        if (other.gameObject.CompareTag("Character"))
        {
            bittingPause = true;
            startCooldown = true;
            
            if (cooldown <= 0)
            {
                Bite(other.transform.position);
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            bittingPause = false;
        }
    }
}
