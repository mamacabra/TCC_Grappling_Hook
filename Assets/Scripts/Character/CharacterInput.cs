using Character.States;
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
            if (CharacterEntity.CharacterState.State is DeathState) return;
            movementInput = context.ReadValue<Vector2>();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.started)
                CharacterEntity.CharacterState.SetDashState();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            var state = CharacterEntity.CharacterState.State;

            if (context.performed && state is WalkState)
                CharacterEntity.CharacterState.SetPrepareHookState();
            else if (context.canceled && state is WalkState or PrepareHookState)
                CharacterEntity.CharacterState.SetDispatchHookState();
        }

        public void OnMelee(InputAction.CallbackContext context)
        {
            if (context.started)
                CharacterEntity.CharacterState.SetAttackState();
        }
    }
}
