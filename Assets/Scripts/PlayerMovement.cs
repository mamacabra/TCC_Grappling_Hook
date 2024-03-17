using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GrapplingHookShoot grapplingHookShoot;
    private Rigidbody myRigidbody;
    private CharacterController characterController;
    Vector2 movementInput = Vector2.zero;
    public bool shooting;
    public float shooting_value;
    public int life = 5;

    [SerializeField] private float speed = 18;
    [SerializeField] private float rotationSpeed = 500;
    [SerializeField] private float dashDistance = 9f;
    [SerializeField] private float dashDuration = 0.1f;

    private bool isDashing = false;

    private Vector3 forwardDirection;
    private Vector3 dashDirection;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        
    }

    public virtual void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        Dash(forwardDirection);
        isDashing = context.action.triggered;
    }

    void Update()
    {
        Vector3 moveDir = new Vector3(movementInput.x, 0, movementInput.y);
        Vector3 movePos = new Vector3(movementInput.x, Physics.gravity.y, movementInput.y);
        moveDir.Normalize();

        if (!grapplingHookShoot.isShooting && !isDashing)
        {
            // Normal movement
            characterController.Move(movePos * Time.deltaTime * speed);

            // Rotation
            if (moveDir != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
            }
        }

        forwardDirection = transform.forward;

        // Dash
        if (isDashing)
        {
            characterController.Move(dashDirection * dashDistance / dashDuration * Time.deltaTime);
        }

        // Shoot
        if (shooting)
        {
            grapplingHookShoot.ShotGrappling();
        }

        if (life <= 0)
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

    public void Dash(Vector3 direction)
    {
        if (!isDashing)
        {

            isDashing = true;
            dashDirection = direction.normalized;
            Invoke("StopDash", dashDuration);

        }
    }

    private void StopDash()
    {
        isDashing = false;
    }
}
