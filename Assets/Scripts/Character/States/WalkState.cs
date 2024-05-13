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
            var moveDirection = new Vector3(axes.x, 0, axes.y);

            if (_hasHit == false)
            {
                Transform.Translate(moveDirection * (WalkSpeed * Time.deltaTime));
            }

            var speed = moveDirection.magnitude;
            if (CharacterEntity.CharacterMesh.animator)
            {
                CharacterEntity.CharacterMesh.animator.SetFloat("Speed", speed);
            }
        }

        public override void FixedUpdate()
        {
            RaycastTest();

            var axes = CharacterEntity.CharacterInput.movementInput;
            var moveDirection = new Vector3(axes.x, 0, axes.y);
            var lookDirection = Transform.position + moveDirection;
            if (lookDirection != Vector3.zero)
            {
                CharacterEntity.Character.characterBody.LookAt(lookDirection);
            }
            // else
            // {
            //     CharacterEntity.Rigidbody.Sleep();
            // }
        }

        private void RaycastTest()
        {
            var axes = CharacterEntity.CharacterInput.movementInput;
            var moveDirection = new Vector3(axes.x, 0, axes.y);
            var position = Transform.position;
            var direction = moveDirection;
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
