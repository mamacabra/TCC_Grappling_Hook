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
            if (CharacterEntity.IsDebug) return;
            movementInput = context.ReadValue<Vector2>();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (CharacterEntity.IsDebug) return;
            CharacterEntity.CharacterState.SetDashHookState();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (CharacterEntity.IsDebug) return;
            //if (CharacterEntity.CharacterState.State is WalkState)
            //{
            //    CharacterEntity.CharacterState.SetDispatchHookState();
            //}

            if(context.performed)
            {
                CharacterEntity.CharacterState.SetPrepareHookState();
            }else if (context.canceled)
            {
                CharacterEntity.CharacterState.SetDispatchHookState();
            }
        }
        
    }
}
