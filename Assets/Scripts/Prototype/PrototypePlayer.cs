using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrototypePlayer : MonoBehaviour
{
    private Rigidbody rigidbody;
    private CharacterController characterController;
    Vector2 movementInput = Vector2.zero;

    [SerializeField]private float speed = 10;
    [SerializeField]private float rotationSpeed = 500;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    void Update()
    {
        Vector3 moveDir = new Vector3(movementInput.x, 0, movementInput.y);
        moveDir.Normalize();

        characterController.Move(moveDir * Time.deltaTime * speed);

        //Rotation
        if(moveDir != Vector3.zero){
            Quaternion toRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
