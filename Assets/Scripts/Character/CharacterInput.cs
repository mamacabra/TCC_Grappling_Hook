using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterInput : MonoBehaviour
    {
        private CharacterEntity _characterEntity;
        public Vector2 movementInput = Vector2.zero;

        public void Setup(CharacterEntity entity)
        {
            _characterEntity = entity;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            movementInput = context.ReadValue<Vector2>();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            _characterEntity.CharacterState.SetDashHookState();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            _characterEntity.CharacterState.SetDispatchHookState();
        }
    }
}
