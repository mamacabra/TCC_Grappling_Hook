using UnityEngine;
using Character.States;
using Character.Utils;

namespace Character
{
    public class Character : MonoBehaviour
    {
        private CharacterState _state;
        public CharacterEntity CharacterEntity;

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
            CharacterEntity = entity;
            SetWalkState();
        }

        private void SetState(CharacterState state)
        {
            _state = state;
            _state.Enter();
            CharacterEntity.CharacterUI.UpdateStatusUI(_state.ToString());
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
