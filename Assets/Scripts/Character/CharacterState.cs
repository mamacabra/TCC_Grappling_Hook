using UnityEngine;
using Character.States;

namespace Character
{
    public class CharacterState : MonoBehaviour
    {
        private ACharacterState _state;
        private CharacterEntity _characterEntity;

        private void Update()
        {
            _state?.Update();
        }

        private void FixedUpdate()
        {
            _state?.FixedUpdate();
        }

        public void Setup(CharacterEntity entity)
        {
            _characterEntity = entity;
            SetWalkState();
        }

        private void SetState(ACharacterState state)
        {
            _state = state;
            _state.Enter();
            _characterEntity.CharacterUI.UpdateStatusUI(_state.ToString());
        }

        public void SetDashHookState()
        {
            var state = new DashState(_characterEntity);
            SetState(state);
        }

        public void SetDispatchHookState()
        {
            var state = new DispatchHookState(_characterEntity);
            SetState(state);
        }

        public void SetPrepareHookState()
        {
            var state = new PrepareHookState(_characterEntity);
            SetState(state);
        }

        public void SetRollbackHookState()
        {
            var state = new RollbackHookState(_characterEntity);
            SetState(state);
        }

        public void SetWalkState()
        {
            var state = new WalkState(_characterEntity);
            SetState(state);
        }
    }
}
