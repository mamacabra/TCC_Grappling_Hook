using Character.States;
using Character.Utils;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Character
{
    public class CharacterInput : ACharacterMonoBehaviour
    {
        public Melee.MeleeAttack meleeAttack;
        public Vector2 movementInput = Vector2.zero;

        public void OnMove(InputAction.CallbackContext context)
        {
            if (CharacterEntity.IsDebug) return;
            if (CharacterEntity.CharacterState.State is DeathState) return;
            movementInput = context.ReadValue<Vector2>();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (CharacterEntity.IsDebug) return;

            var state = CharacterEntity.CharacterState.State;
            if (state is DeathState) return;
            if (context.started) CharacterEntity.CharacterState.SetDashState();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (CharacterEntity.IsDebug) return;

            var state = CharacterEntity.CharacterState.State;
            if (state is DeathState) return;
            if (context.performed && state is WalkState)
                CharacterEntity.CharacterState.SetPrepareHookState();
            else if (context.canceled && state is WalkState or PrepareHookState)
                CharacterEntity.CharacterState.SetDispatchHookState();
        }

        public void OnMelee(InputAction.CallbackContext context)
        {
            if (CharacterEntity.IsDebug) return;

            var state = CharacterEntity.CharacterState.State;
            if (state is DeathState) return;
            if (context.started) {
                meleeAttack.ActivateHitbox();
                CharacterEntity.CharacterMesh.animator?.SetTrigger("Melee");
            }
        }
    }
}
