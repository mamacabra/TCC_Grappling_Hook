using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class WalkState : ACharacterState
    {
        private bool hasHit;
        private const float RaycastDistance = 1f;
        private Color RaycastColor => hasHit ? Color.red : Color.green;

        public WalkState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.GrapplingHookWeapon.ResetHook();
        }

        public override void Update()
        {
            RaycastTest();
            if (hasHit == false) Walk();
            LookAt();
        }

        private void RaycastTest()
        {
            var axes = CharacterEntity.CharacterInput.Axes;
            var moveDirection = new Vector3(axes.x, 0, axes.y);
            var position = Transform.position;
            var direction = moveDirection;
            var origin = new Vector3(position.x, 1f, position.z) + direction;

            Physics.Raycast(origin, direction, out var hit, RaycastDistance);
            Debug.DrawRay(origin, direction * RaycastDistance, RaycastColor);

            if (hit.collider)
            {
                hasHit = hit.collider.CompareTag(Const.Tags.Wall) || hit.collider.CompareTag(Const.Tags.Object);
            }
            else
            {
                hasHit = false;
            }
        }
    }
}
