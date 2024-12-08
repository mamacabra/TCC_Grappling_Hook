using Character.Utils;
using UnityEngine;
using VFX;

namespace Character.GrapplingHook.States
{
    public class HookDispatchState : AGrapplingHookState
    {
        private float hookSpeed = 80;
        private float hookMaxDistance = 24;
        private const float VFXPositionOffset = 2f;

        public HookDispatchState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            SetHookStats();
            EnableHookCollider();
            SetHookColliderSize(GrapplingStats.HookColliderDispatchSize);

            var characterTransform = CharacterEntity.Character.transform;
            var vfxPosition = characterTransform.position + characterTransform.forward * VFXPositionOffset;
            VFXManager.Instance.PlayHookDispatchVFX(vfxPosition);
            CharacterEntity.GrapplingHookRope.SetActive(true);
        }

        public override void FixedUpdate()
        {
            var transform = CharacterEntity.GrapplingHookTransform;
            transform.Translate(Vector3.forward * (Time.fixedDeltaTime * hookSpeed));

            var hookDistance = Vector3.Distance(GrapplingStats.OriginPosition, transform.localPosition);
            if (hookDistance >= hookMaxDistance)
                CharacterEntity.CharacterState.SetRollbackHookState();
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
