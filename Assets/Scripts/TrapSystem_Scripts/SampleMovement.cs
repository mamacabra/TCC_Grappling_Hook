using System.Collections;
using System.Collections.Generic;
using TrapSystem_Scripts;
using TrapSystem_Scripts.ModifierSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class SampleMovement : MonoBehaviour, IModifyable
{
    public float acceleration;
    public Vector3 targetSpeed;
    public float speed = 20.0f;
    public Vector3 currentSpeed = Vector3.zero;

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
        targetSpeed = dir.normalized * Mathf.MoveTowards(currentSpeed.magnitude, speed, 50 * Time.deltaTime);
        acceleration = speed;

        foreach (var modifier in Modifiers) {
            if (modifier is MovementModifier) modifier.ApplyModifier(ref targetSpeed, ref acceleration, dir);
        }

        currentSpeed = Vector3.MoveTowards(currentSpeed, targetSpeed, acceleration);

        if (dir != Vector3.zero) {
            var lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, lookRotation, 15 * Time.deltaTime);
        }

        transform.Translate(currentSpeed * Time.deltaTime, Space.World);
    }
}
