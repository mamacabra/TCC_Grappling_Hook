using Character.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterInput : ACharacterMonoBehaviour
    {
        public Vector2 movementInput = Vector2.zero;

        public void OnMove(InputAction.CallbackContext context)
        {
            movementInput = context.ReadValue<Vector2>();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.started)
                CharacterEntity.CharacterState.SetDashState();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.performed)
                CharacterEntity.CharacterState.SetPrepareHookState();
            else if (context.canceled)
                CharacterEntity.CharacterState.SetDispatchHookState();
        }

        public void OnMelee(InputAction.CallbackContext context)
        {
            if (context.started)
                CharacterEntity.CharacterState.SetAttackMeleeState();
        }
    }
}
