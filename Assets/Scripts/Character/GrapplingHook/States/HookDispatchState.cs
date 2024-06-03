using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook.States
{
    public class HookDispatchState : AGrapplingHookState
    {
        private float hookSpeed = 80;
        private float hookMaxDistance = 24;

        public HookDispatchState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Enter()
        {
            SetHookStats();
            CharacterEntity.GrapplingHookCollider.enabled = true;
        }

        public override void FixedUpdate()
        {
            var transform = CharacterEntity.GrapplingHookTransform;
            transform.Translate(Vector3.forward * (Time.fixedDeltaTime * hookSpeed));

            var hookDistance = Vector3.Distance(GrapplingStats.originPosition, transform.localPosition);
            if (hookDistance >= hookMaxDistance)
                CharacterEntity.CharacterState.SetRollbackHookState();
        }

        public override void Exit()
        {
            CharacterEntity.GrapplingHookCollider.enabled = false;
        }

        private void SetHookStats()
        {
            switch (CharacterEntity.Hook.Force)
            {
                case 1:
                    hookSpeed = GrapplingStats.ForceLv1.speed;
                    hookMaxDistance = GrapplingStats.ForceLv1.distance;
                    break;
                case 2:
                    hookSpeed = GrapplingStats.ForceLv2.speed;
                    hookMaxDistance = GrapplingStats.ForceLv2.distance;
                    break;
                default:
                    hookSpeed = GrapplingStats.ForceLv3.speed;
                    hookMaxDistance = GrapplingStats.ForceLv3.distance;
                    break;
            }
        }
    }
}
