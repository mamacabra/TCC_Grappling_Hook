using Character.GrapplingHook.States;
using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook
{
    public class GrapplingHookState : ACharacterMonoBehaviour
    {
        public AGrapplingHookState State { get; private set; }

        private void Update()
        {
            State?.Update();
        }

        private void FixedUpdate()
        {
            State?.FixedUpdate();
        }

        private void SetState(AGrapplingHookState state)
        {
            State?.Exit();
            State = state;
            State.Enter();
        }

        public void SetHookDestroyedState()
        {
            var state = new HookDestroyedState(CharacterEntity);
            SetState(state);
        }

        public void SetHookDispatchState()
        {
            var state = new HookDispatchState(CharacterEntity);
            SetState(state);
        }

        public void SetHookFixWallState(Vector3 wallPoint)
        {
            var state = new HookFixWallState(CharacterEntity, wallPoint);
            SetState(state);
        }

        public void SetHookReadyState()
        {
            var state = new HookReadyState(CharacterEntity);
            SetState(state);
        }

        public void SetHookRollbackState()
        {
            var state = new HookRollbackState(CharacterEntity);
            SetState(state);
        }
    }
}
