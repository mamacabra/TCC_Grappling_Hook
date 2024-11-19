using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using LocalMultiplayer;

public class PrototypeCameraMoviment : MonoBehaviour
{
    private void Start() {
        PlayersManager.Instance.cameraMovement = this;
    }

    public void RecivePlayers(Transform p)
    {
        players.Add(p);
        cinemachineTargetGroup.AddMember(p,1,2);
    }
    public void RemovePlayers(Transform p)
    {
        players.Remove(p);
        cinemachineTargetGroup.RemoveMember(p);
    }
    public void RemoveAllPlayers()
    {
        foreach (var p in players)
            cinemachineTargetGroup.RemoveMember(p);
        
        players.Clear();
        
    }
    public List<Transform> players = new List<Transform>(); // Array dos Transforms dos personagens
    public CinemachineTargetGroup cinemachineTargetGroup;

    /*public float minZoom = 5f; // Zoom mínimo da câmera
    public float maxZoom = 15f; // Zoom máximo da câmera
    public float zoomSpeed = 2f; // Velocidade de zoom da câmera
    public float moveSpeed = 5f; // Velocidade de movimento da câmera
    public Vector3 offset; // Offset da câmera em relação aos personagens

    public Camera cam;
    private Vector3 desiredPosition;

    void LateUpdate()
    {
        if (players.Count == 0)
            return;

        Move();
        Zoom();
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        desiredPosition = centerPoint + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, moveSpeed * Time.deltaTime);
    }

    void Zoom()
    {
        float distance = GetGreatestDistance();
        float newZoom = Mathf.Lerp(maxZoom, minZoom, distance / 10f);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime * zoomSpeed);
    }

    Vector3 GetCenterPoint()
    {
        if (players.Count == 1)
            return players[0].position;

        Bounds bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (Transform p in players)
        {
            bounds.Encapsulate(p.position);
        }

        return bounds.center;
    }

    float GetGreatestDistance()
    {
        Bounds bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (Transform p in players)
        {
            bounds.Encapsulate(p.position);
        }

        return bounds.size.magnitude;
    }*/
}