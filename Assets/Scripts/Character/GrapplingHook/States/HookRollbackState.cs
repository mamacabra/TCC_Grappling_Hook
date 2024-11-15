using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook.States
{
    public class HookRollbackState : AGrapplingHookState
    {
        public HookRollbackState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            EnableHookCollider();
            SetHookColliderSize(GrapplingStats.HookColliderRollbackSize);
        }

        public override void FixedUpdate()
        {
            var transform = CharacterEntity.GrapplingHookTransform;
            transform.Translate(Vector3.forward * (-1f * (Time.fixedDeltaTime * GrapplingStats.RollbackSpeed)));

            var hookDistance = Vector3.Distance(GrapplingStats.OriginPosition, transform.localPosition);
            if (hookDistance <= 0.1f || transform.localPosition.z <= GrapplingStats.OriginPosition.z)
                CharacterEntity.CharacterState.SetWalkState();
        }

        public override void Exit()
        {
            SetHookColliderSize(GrapplingStats.HookColliderDispatchSize);
        }
    }
}
