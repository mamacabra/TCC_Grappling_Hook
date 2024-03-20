using Character.States;
using Character.Utils;

namespace Character
{
    public class CharacterState : ACharacterMonoBehaviour
    {
        private ACharacterState State { get; set; }

        private void Update()
        {
            State?.Update();
        }

        private void FixedUpdate()
        {
            State?.FixedUpdate();
        }

        private void SetState(ACharacterState state)
        {
            State = state;
            State.Enter();
            CharacterEntity.CharacterUI.UpdateCharacterStateUI(State.GetType().Name);
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
