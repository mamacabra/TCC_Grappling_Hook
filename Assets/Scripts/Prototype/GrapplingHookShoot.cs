using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookShoot : MonoBehaviour
{
    
    public Transform grapStartPosition;
    public Transform grapShotPosition;
   
    Rigidbody rb;
    
    public float grapplingMaximumDistance=5;
    public float grapplingSpeed = 10f;
    private float shotActualdistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    private void Update()
    {
        shotActualdistance = Vector3.Distance(grapShotPosition.position, grapStartPosition.transform.position);
    }

    public void ShotGrappling()
    {
        Vector3 direction = transform.forward;
        
        if (rb != null ) 
        {
            rb.velocity = direction * grapplingSpeed;
        }else if(shotActualdistance >= grapplingMaximumDistance) 
        {
            rb.velocity = -direction * grapplingSpeed;
        }
        if(shotActualdistance <= 0) 
        {
            rb.velocity = direction * 0;
        }
        
        
        

        Debug.Log("Shot");


    }
        

    
}
