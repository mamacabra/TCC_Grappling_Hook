using System.Linq;
using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook.States
{
    public class HookDispatchState : AGrapplingHookState
    {
        private float hookSpeed = 80;
        private float hookMaxDistance = 24;
        private Vector3 debugRayOriginPosition;

        public HookDispatchState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            SetHookStats();
            EnableHookCollider();
            FindAndLookEnemyDirection();

            debugRayOriginPosition = CharacterEntity.GrapplingHookTransform.position;
        }

        public override void FixedUpdate()
        {
            var transform = CharacterEntity.GrapplingHookTransform;
            transform.Translate(Vector3.forward * (Time.fixedDeltaTime * hookSpeed));

            var hookDistance = Vector3.Distance(GrapplingStats.originPosition, transform.localPosition);
            if (hookDistance >= hookMaxDistance)
                CharacterEntity.CharacterState.SetRollbackHookState();

            RayCastDebug();
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

        private void FindAndLookEnemyDirection()
        {
            var transform = CharacterEntity.GrapplingHookTransform;
            Vector3[] directions =
            {
                (transform.forward * 16 + transform.right).normalized,
                (transform.forward * 16 + transform.right * -1).normalized,
                (transform.forward * 8 + transform.right).normalized,
                (transform.forward * 8 + transform.right * -1).normalized,
            };

            var direction = directions.Select(RayCastToEnemy).FirstOrDefault();
            if (direction != Vector3.zero)
                CharacterEntity.Character.LookAt(direction);
        }

        private Vector3 RayCastToEnemy(Vector3 direction)
        {
            var transform = CharacterEntity.GrapplingHookTransform;
            var origin = new Vector3(transform.position.x, 1f, transform.position.z);
            var hits = Physics.RaycastAll(origin, direction, 100f);
            if (hits.Length == 0) return Vector3.zero;

            var distance = Mathf.Infinity;
            foreach (var hit in hits)
            {
                var distanceToHit = Vector3.Distance(origin, hit.point);
                if (!(distanceToHit < distance)) continue;

                distance = distanceToHit;
                if (hit.collider.gameObject.CompareTag(Const.Tags.Character))
                    return hit.point;
            }

            return Vector3.zero;
        }

        private void RayCastDebug()
        {
            var transform = CharacterEntity.GrapplingHookTransform;
            var origin = new Vector3(debugRayOriginPosition.x, 1f, debugRayOriginPosition.z);

            var dir1 = (transform.forward * 8 + transform.right).normalized;
            Debug.DrawRay(origin, dir1 * hookMaxDistance, Color.red);
            var dir2 = (transform.forward * 8 + transform.right * -1).normalized;
            Debug.DrawRay(origin, dir2 * hookMaxDistance, Color.red);
            var dir3 = (transform.forward * 16 + transform.right).normalized;
            Debug.DrawRay(origin, dir3 * hookMaxDistance, Color.red);
            var dir4 = (transform.forward * 16 + transform.right * -1).normalized;
            Debug.DrawRay(origin, dir4 * hookMaxDistance, Color.red);
        }
    }
}
