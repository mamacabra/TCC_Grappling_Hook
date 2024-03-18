using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterMovement : MonoBehaviour
    {
        private CharacterEntity _characterEntity;
        public Vector2 movementInput = Vector2.zero;

        public float speed = 18;
        public float rotationSpeed = 500;

        public void OnMove(InputAction.CallbackContext context)
        {
            movementInput = context.ReadValue<Vector2>();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            _characterEntity.Character.SetDashHookState();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            _characterEntity.Character.SetDispatchHookState();
        }

        public void Setup(CharacterEntity entity)
        {
            _characterEntity = entity;
        }
    }
}
