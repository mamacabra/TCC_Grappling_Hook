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
    }
}
