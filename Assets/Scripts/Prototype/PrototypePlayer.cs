using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrototypePlayer : MonoBehaviour
{
    public GrapplingHookShoot grapplingHookShoot;
    private Rigidbody myRigidbody;
    
    Vector2 movementInput = Vector2.zero;
    public bool shooting; 
    public float shooting_value;
    public int life = 5;
    

    [SerializeField]private float speed = 10;
    [SerializeField]private float rotationSpeed = 500;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
       
    }

    public void OnMove(InputAction.CallbackContext context)
    { 
        movementInput = context.ReadValue<Vector2>();   
    }
    void Update()
    {
        Vector3 moveDir = new Vector3(movementInput.x, 0, movementInput.y);
        Vector3 movePos = new Vector3(movementInput.x, Physics.gravity.y, movementInput.y);
        moveDir.Normalize();
        if (!grapplingHookShoot.isShooting)
        {
            
            myRigidbody.MovePosition(transform.position+moveDir*Time.deltaTime*speed);

            //Rotation
            if (moveDir != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
            }

            //Shoot
            if (shooting)
            {
                grapplingHookShoot.ShotGrappling();
            }
        }

        if(life<=0)
        {
           
        }
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        shooting = context.ReadValueAsButton();
    }

    public void ReceiveDamage() 
    {
        life--;
    }

    
}
