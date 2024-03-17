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
            _state.Update();

            if (Input.GetKeyDown(KeyCode.Space))
                SetPrepareHookState();
            else if (Input.GetKeyUp(KeyCode.Space) && _state is PrepareHookState)
                SetDispatchHookState();
        }

        private void FixedUpdate()
        {
            _state.FixedUpdate();
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

        private void SetDispatchHookState()
        {
            var state = new DispatchHookState(this);
            SetState(state);
        }

        private void SetPrepareHookState()
        {
            var state = new PrepareHookState(this);
            SetState(state);
        }

        public void SetRollbackHookState()
        {
            var state = new RollbackHookState(this);
            SetState(state);
        }

        public void SetWalkState()
        {
            var state = new WalkState(this);
            SetState(state);
        }
    }
}
