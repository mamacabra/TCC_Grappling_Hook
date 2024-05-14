using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class WalkState : ACharacterState
    {
        private const float WalkSpeed = 16f;

        private bool _hasHit;
        private const float RaycastDistance = 1f;
        private Color RaycastColor => _hasHit ? Color.red : Color.green;

        public WalkState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.GrapplingHookWeapon.ResetHook();
        }

        public override void Update()
        {
            var axes = CharacterEntity.CharacterInput.movementInput;
            var direction = new Vector3(axes.x, 0, axes.y);

            if (_hasHit == false)
            {
                CharacterEntity.Rigidbody.MovePosition(CharacterEntity.Rigidbody.transform.position + direction * (WalkSpeed * Time.fixedDeltaTime));
            }

            var speed = direction.magnitude;
            if (CharacterEntity.CharacterMesh.animator)
            {
                CharacterEntity.CharacterMesh.animator.SetFloat("Speed", speed);
            }
        }

        public override void FixedUpdate()
        {
            RaycastTest();

            var axes = CharacterEntity.CharacterInput.movementInput;
            var direction = new Vector3(axes.x, 0, axes.y) + Transform.position;
            if (direction != Vector3.zero)
            {
                Transform.LookAt(direction);
            }
            else
            {
                if (!CharacterEntity.Rigidbody.IsSleeping()) CharacterEntity.Rigidbody.velocity = Vector3.zero;
                CharacterEntity.Rigidbody.Sleep();
            }
        }

        private void RaycastTest()
        {
            var direction = Transform.forward;
            var position = Transform.position;
            var origin = new Vector3(position.x, 1f, position.z) + direction;

            Physics.Raycast(origin, direction, out var hit, RaycastDistance);
            Debug.DrawRay(origin, direction * RaycastDistance, RaycastColor);

            if (hit.collider)
            {
                _hasHit = hit.collider.CompareTag(Const.Tags.Wall) || hit.collider.CompareTag(Const.Tags.Object);
            }
            else
            {
                _hasHit = false;
            }
        }
    }
}
