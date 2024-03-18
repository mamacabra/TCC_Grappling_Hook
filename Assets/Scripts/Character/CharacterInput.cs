using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterInput : CharacterMonoBehaviour
    {
        public Vector2 movementInput = Vector2.zero;

        public void OnMove(InputAction.CallbackContext context)
        {
            movementInput = context.ReadValue<Vector2>();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            CharacterEntity.CharacterState.SetDashHookState();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            CharacterEntity.CharacterState.SetDispatchHookState();
        }
    }
}
