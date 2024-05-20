using Character.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterInput : ACharacterMonoBehaviour
    {
        public Vector2 Axes { get; private set; } = Vector2.zero;
        public Vector3 MoveDirection => new (Axes.x, 0, Axes.y);
        public Vector3 LookDirection => transform.position + MoveDirection;

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.started)
                CharacterEntity.CharacterState.SetDashState();
        }

        public void OnMelee(InputAction.CallbackContext context)
        {
            if (context.started)
                CharacterEntity.CharacterState.SetAttackMeleeState();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Axes = context.ReadValue<Vector2>();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.started)
                CharacterEntity.CharacterState.SetPrepareHookState();
            else if (context.canceled)
                CharacterEntity.CharacterState.SetDispatchHookState();
        }
    }
}
