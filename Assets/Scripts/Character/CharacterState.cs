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
            if (State is DeathState) return;
            if (State is not DashState) State?.Exit();

            State = state;
            State.Enter();
            CharacterEntity.CharacterUI.UpdateCharacterStateUI(State.GetType().Name);
        }

        public void SetDashState()
        {
            if (CharacterEntity.Character.HasDashReady == false) return;

            var state = new DashState(CharacterEntity);
            SetState(state);
        }

        public void SetDeathState()
        {
            var state = new DeathState(CharacterEntity);
            SetState(state);
        }

        public void SetDispatchHookState()
        {
            if (CharacterEntity.Character.HasHookReady == false) return;

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

        public void SetPrepareHookState()
        {
            if (CharacterEntity.Character.HasHookReady == false) return;

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

        public void SetAttackState()
        {
            var state = new AttackState(CharacterEntity);
            SetState(state);
        }

        public void SetKnockbackState()
        {
            var state = new KnockbackState(CharacterEntity);
            SetState(state);
        }
    }
}
