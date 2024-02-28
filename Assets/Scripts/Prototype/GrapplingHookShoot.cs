using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookShoot : MonoBehaviour
{
    public PrototypePlayer prototypePlayer;
    
    public Transform grapStartPosition;
    public Transform grapShotPosition;
   
    Rigidbody rb;
    [SerializeField] private TrailRenderer trailRenderer;
    
    public float grapplingMaximumDistance;
    public float grapplingSpeed ;
    public float shotActualdistance;

    public bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    private void Update()
    {
        shotActualdistance = Vector3.Distance(grapShotPosition.position, grapStartPosition.transform.position);
        if (shotActualdistance > 1) 
        {
            isShooting = true;
        }
        if (shotActualdistance > grapplingMaximumDistance ) 
        {
           ReturnHook();
        }

        if (shotActualdistance <= 1f && isShooting)
        {
            PutHookInInitialPosition();
            rb.velocity = Vector3.zero;
        }
        
    }

    public void ShotGrappling()
    {
        Vector3 direction = transform.forward;

        if (rb != null)
        {
            rb.velocity = direction * grapplingSpeed;
            
               
            Debug.Log("Shot");
        }

    }

    public void ReturnHook()
    {
        Vector3 directionR = (grapStartPosition.position - grapShotPosition.position).normalized;
        rb.velocity = directionR * grapplingSpeed;

    }

    public void PutHookInInitialPosition() 
    {
        if (isShooting) 
        {
            grapShotPosition.position = grapStartPosition.position;
            rb.velocity = Vector3.zero;
            isShooting = false;
            Debug.Log("Returned");
        }
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            prototypePlayer.ReceiveDamage();
            //grapShotPosition.position = grapStartPosition.position;
            
            GameObject otherPlayer = collision.gameObject;
            
            Rigidbody otherPlayerRigidbody = otherPlayer.GetComponent<Rigidbody>();

            if (otherPlayerRigidbody != null)
            {
                Vector3 pullDirection=(grapShotPosition.position-otherPlayer.transform.position).normalized;

                otherPlayerRigidbody.AddForce(pullDirection * grapplingSpeed, ForceMode.VelocityChange);
                

            }
        }

    }

}
