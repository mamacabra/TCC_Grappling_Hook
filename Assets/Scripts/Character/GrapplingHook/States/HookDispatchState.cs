using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook.States
{
    public class HookDispatchState : AGrapplingHookState
    {
        private float hookSpeed = 80;
        private float hookMaxDistance = 24;
        private readonly Vector3 hookOriginLocalPosition = new (0f, 1, 1.2f); // TODO: Melhotar isso

        public HookDispatchState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void FixedUpdate()
        {
            var transform = CharacterEntity.GrapplingHookTransform;
            transform.Translate(Vector3.forward * (Time.fixedDeltaTime * hookSpeed));

            var hookDistance = Vector3.Distance(hookOriginLocalPosition, transform.localPosition);
            if (hookDistance >= hookMaxDistance)
                CharacterEntity.CharacterState.SetRollbackHookState();
        }
    }
}
