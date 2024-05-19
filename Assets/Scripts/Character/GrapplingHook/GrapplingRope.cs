using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingRope : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public GameObject ropeStart;
    public GameObject ropeTarget;
    private void Start()
    {
        lineRenderer.positionCount = 2;
    }
    private void Update()
    {
        lineRenderer.SetPosition(0, ropeStart.transform.position);
        lineRenderer.SetPosition(1, ropeTarget.transform.position);
    }
}
