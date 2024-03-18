using Character.States;

namespace Character
{
    public class CharacterState : CharacterMonoBehaviour
    {
        private ACharacterState _state;

        private void Update()
        {
            _state?.Update();
        }

        private void FixedUpdate()
        {
            _state?.FixedUpdate();
        }

        private void SetState(ACharacterState state)
        {
            _state = state;
            _state.Enter();
            CharacterEntity.CharacterUI.UpdateStatusUI(_state.GetType().Name);
        }

        public void SetDashHookState()
        {
            var state = new DashState(CharacterEntity);
            SetState(state);
        }

        public void SetDispatchHookState()
        {
            var state = new DispatchHookState(CharacterEntity);
            SetState(state);
        }

        public void SetPrepareHookState()
        {
            var state = new PrepareHookState(CharacterEntity);
            SetState(state);
        }

        public void SetRollbackHookState()
        {
            var state = new RollbackHookState(CharacterEntity);
            SetState(state);
        }

        public void SetWalkState()
        {
            var state = new WalkState(CharacterEntity);
            SetState(state);
        }
    }
}
