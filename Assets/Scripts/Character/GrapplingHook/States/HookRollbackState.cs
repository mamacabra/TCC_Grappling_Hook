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
        }

        public override void FixedUpdate()
        {
            var transform = CharacterEntity.GrapplingHookTransform;
            transform.Translate(Vector3.forward * (-1f * (Time.fixedDeltaTime * GrapplingStats.RollbackSpeed)));

            var hookDistance = Vector3.Distance(GrapplingStats.originPosition, transform.localPosition);
            if (hookDistance <= 0.1f || transform.localPosition.z <= GrapplingStats.originPosition.z)
                CharacterEntity.CharacterState.SetWalkState();
        }
    }
}
