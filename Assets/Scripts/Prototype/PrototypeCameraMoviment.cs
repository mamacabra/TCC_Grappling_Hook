using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeCameraMoviment : MonoBehaviour
{
    [SerializeField] private List<Transform> players = new List<Transform>();
    [SerializeField] private float minDistance = 5.0f;
    [SerializeField] private float maxDistance = 10.0f;
    [SerializeField] private float rotationX = 80.0f; 
    [SerializeField] private float smoothness = 5.0f;

    public void RecivePlayers(Transform p)
    {
        players.Add(p);
    }

    void Update()
    {
        if (players.Count >= 2)
        {
            float maxDistanceBetweenPlayers = FindMaxDistanceBetweenPlayers();
            
            Vector3 midpoint = FindMidpointBetweenPlayers();
            
            float targetDistance = Mathf.Lerp(minDistance, maxDistance, Mathf.InverseLerp(0, maxDistance, maxDistanceBetweenPlayers));
            Vector3 targetPosition = midpoint - transform.forward * targetDistance;
            
            Quaternion targetRotation = Quaternion.Euler(rotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothness);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothness);
        }
    }

    float FindMaxDistanceBetweenPlayers()
    {
        float maxDistance = 0;

        for (int i = 0; i < players.Count; i++)
        {
            for (int j = i + 1; j < players.Count; j++)
            {
                float distance = Vector3.Distance(players[i].position, players[j].position);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }
        }

        return maxDistance;
    }

    Vector3 FindMidpointBetweenPlayers()
    {
        Vector3 minPosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 maxPosition = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        
        foreach (Transform player in players)
        {
            minPosition = Vector3.Min(minPosition, player.position);
            maxPosition = Vector3.Max(maxPosition, player.position);
        }

        // Calcula o ponto médio entre as posições mínima e máxima
        return (minPosition + maxPosition) / 2.0f;
    }
}