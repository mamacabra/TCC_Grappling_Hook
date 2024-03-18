using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterMovement : MonoBehaviour
    {
        private CharacterEntity _characterEntity;

        // public GrapplingHookShoot grapplingHookShoot;
        public Vector2 movementInput = Vector2.zero;
        public bool shooting;
        public float shooting_value;
        public int life = 5;

        public float speed = 18;
        public float rotationSpeed = 500;
        public static float dashDistance = 9f;
        public static float dashDuration = 0.1f;

        private bool isDashing = false;

        private Vector3 forwardDirection;
        private Vector3 dashDirection;

        private void Update()
        {
            forwardDirection = transform.forward;

            // Dash
            // if (isDashing)
            // {
            //     _characterEntity.CharacterController.Move(dashDirection * dashDistance / dashDuration * Time.deltaTime);
            // }

            // Shoot
            // if (shooting)
            // {
            //     grapplingHookShoot.ShotGrappling();
            // }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            movementInput = context.ReadValue<Vector2>();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            // Dash(forwardDirection);
            // isDashing = context.action.triggered;
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            _characterEntity.Character.SetDispatchHookState();
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

        public void Setup(CharacterEntity entity)
        {
            _characterEntity = entity;
        }
    }
}
