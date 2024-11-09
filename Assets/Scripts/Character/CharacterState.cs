using Character.States;
using Character.Utils;
using UnityEngine;

namespace Character
{
    public class CharacterState : ACharacterMonoBehaviour
    {
        public ACharacterState State { get; private set; }

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
            switch (State)
            {
                case DeathState or WinnerState:
                case LoserState when state is not WinnerState:
                    return;
            }

            State?.Exit();
            State = state;
            State.Enter();
            CharacterEntity.CharacterUI.UpdateCharacterStateUI(State.ToString());
        }

        public void SetAttackMeleeState()
        {
            if (CharacterEntity.Character.HasAttackReady == false) return;
            if (CharacterEntity.CharacterState.State is AttackState or DashState or HookedToEnemyState or PainState or ReadyState) return;
            if (Time.deltaTime == 0) return;

            var state = new AttackState(CharacterEntity);
            SetState(state);
        }

        public void SetCaughtEnemyState(CharacterEntity enemy)
        {
            var state = new CaughtEnemyState(CharacterEntity, enemy);
            SetState(state);
        }

        public void SetDashState()
        {
            if (CharacterEntity.Character.HasDashReady == false) return;
            if (CharacterEntity.CharacterState.State is AttackState or HookedToEnemyState or PainState or ReadyState) return;
            if (Time.deltaTime == 0) return;

            var state = new DashState(CharacterEntity);
            SetState(state);
        }

        public void SetDeathState(Transform killedBy)
        {
            var state = new DeathState(CharacterEntity, killedBy);
            SetState(state);
        }

        public void SetDispatchHookState()
        {
            if (CharacterEntity.CharacterState.State is not PrepareHookState) return;
            if (Time.deltaTime == 0) return;

            var state = new DispatchHookState(CharacterEntity);
            SetState(state);
        }

        public void SetHookedToEnemyState(Vector3 enemyPosition)
        {
            var state = new HookedToEnemyState(CharacterEntity, enemyPosition);
            SetState(state);
        }

        public void SetHookedToWallState(Vector3 wallPoint)
        {
            var state = new HookedToWallState(CharacterEntity, wallPoint);
            SetState(state);
        }

        public void SetLoserState()
        {
            var state = new LoserState(CharacterEntity);
            SetState(state);
        }

        public void SetPainState()
        {
            var state = new PainState(CharacterEntity);
            SetState(state);
        }

        public void SetParryAttackState()
        {
            var state = new ParryAttackState(CharacterEntity);
            SetState(state);
        }

        public void SetParryHookState()
        {
            var state = new ParryHookState(CharacterEntity);
            SetState(state);
        }

        public void SetPrepareHookState()
        {
            if (CharacterEntity.CharacterState.State is not WalkState) return;
            if (Time.deltaTime == 0) return;

            var state = new PrepareHookState(CharacterEntity);
            SetState(state);
        }

        public void SetReadyState()
        {
            var state = new ReadyState(CharacterEntity);
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

        public void SetWinnerState()
        {
            var state = new WinnerState(CharacterEntity);
            SetState(state);
        }
    }
}
