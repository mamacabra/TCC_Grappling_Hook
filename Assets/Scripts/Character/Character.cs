using UnityEngine;
using Character.States;
using Character.GrapplingHook;
using Character.Utils;

namespace Character
{
    public class Character : MonoBehaviour
    {
        private CharacterState _state;
        public GrapplingHookWeapon grapplingHookWeapon;

        private void Update()
        {
            _state.Update();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetPrepareHookState();
            }

            if (Input.GetKeyUp(KeyCode.Space) && _state is PrepareHookState)
            {
                SetDispatchHookState();

                // TODO: Remover quando todo ciclo de estados estiver finalizado
                grapplingHookWeapon.ResetForce();
                SetWalkState();
            }
        }

        private void FixedUpdate()
        {
            _state.FixedUpdate();
        }

        public void Setup(CharacterEntity entity)
        {
            grapplingHookWeapon = entity.grapplingHookWeapon;
            SetWalkState();
        }

        private void SetState(CharacterState state)
        {
            _state = state;
            _state.Enter();
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

        private void SetWalkState()
        {
            var state = new WalkState(this);
            SetState(state);
        }
    }
}
