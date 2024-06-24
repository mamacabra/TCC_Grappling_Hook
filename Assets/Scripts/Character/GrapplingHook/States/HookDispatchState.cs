using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook.States
{
    public class HookDispatchState : AGrapplingHookState
    {
        private float hookSpeed = 80;
        private float hookMaxDistance = 24;

        private bool hasHitLeft;
        private Color RaycastColorLeft => hasHitLeft ? Color.red : Color.green;
        private bool hasHitCenter;
        private Color RaycastColorCenter => hasHitCenter ? Color.red : Color.green;
        private bool hasHitRight;
        private Color RaycastColorRight => hasHitRight ? Color.red : Color.green;

        public HookDispatchState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            SetHookStats();
            EnableHookCollider();
        }

        public override void FixedUpdate()
        {
            var transform = CharacterEntity.GrapplingHookTransform;
            transform.Translate(Vector3.forward * (Time.fixedDeltaTime * hookSpeed));

            var hookDistance = Vector3.Distance(GrapplingStats.originPosition, transform.localPosition);
            if (hookDistance >= hookMaxDistance)
                CharacterEntity.CharacterState.SetRollbackHookState();



            var rayLeftDirection = (Vector3.forward + Vector3.right * -1).normalized;
            // Physics.Raycast(GrapplingStats.originPosition, rayLeftDirection, out var hitLeft, hookMaxDistance);
            Debug.DrawRay(GrapplingStats.originPosition, rayLeftDirection * hookMaxDistance, RaycastColorLeft);

            var rayCenterDirection = Vector3.forward;
            // Physics.Raycast(GrapplingStats.originPosition, rayCenterDirection, out var hitCenter, hookMaxDistance);
            Debug.DrawRay(GrapplingStats.originPosition, rayCenterDirection * hookMaxDistance, RaycastColorCenter);

            var rayRightDirection = (Vector3.forward + Vector3.right).normalized;
            // Physics.Raycast(GrapplingStats.originPosition, rayRightDirection, out var hitRight, hookMaxDistance);
            Debug.DrawRay(GrapplingStats.originPosition, rayRightDirection * hookMaxDistance, RaycastColorRight);
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
