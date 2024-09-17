using System.Collections;
using System.Collections.Generic;
using TrapSystem_Scripts;
using TrapSystem_Scripts.ModifierSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class SampleMovement : MonoBehaviour, IModifyable
{
    public float acceleration = 2.0f;
    public float maxSpeed = 10.0f;
    public float speed = 5.0f;
    [SerializeField] private Vector3 currentSpeed = Vector3.zero;

    [SerializeField] Vector2 Axes;

    public List<AModifier> Modifiers => modifiers;
    [SerializeField] private List<AModifier> modifiers = new();

    void Update() {
        MoveDirection(new Vector3(Axes.x, 0.0f, Axes.y));
    }

    public void OnMove(InputAction.CallbackContext context) {
        Axes = context.ReadValue<Vector2>();
    }

    void MoveDirection(Vector3 dir) {
        
        if (((IModifyable)this).TryGetModifier(out AccelerationModifier modifier)) {
            // Calculate the target speed based on the direction and max speed
            Vector3 targetSpeed = dir.normalized * modifier.maxSpeed;

            // Gradually increase the current speed towards the target speed
            currentSpeed = Vector3.MoveTowards(currentSpeed, targetSpeed, modifier.acceleration * Time.deltaTime);
        }
        else currentSpeed = dir.normalized * speed;

        transform.Translate(currentSpeed * Time.deltaTime);
    }
}
