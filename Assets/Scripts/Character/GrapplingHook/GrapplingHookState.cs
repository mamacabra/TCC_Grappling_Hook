using Character.GrapplingHook.States;
using Character.Utils;

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

        public void SetHookDispatchState()
        {
            var state = new HookDispatchState(CharacterEntity);
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