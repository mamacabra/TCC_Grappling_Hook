using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class WalkState : ACharacterState
    {
        private const float WalkSpeed = 15f;
        private const float RotationSpeed = 1000f;

        private float movementMagnitude;

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
            var movementInput = CharacterEntity.CharacterInput.movementInput;
            var direction = new Vector3(movementInput.x, 0, movementInput.y);

            if (_hasHit == false)
            {
                CharacterEntity.Rigidbody.MovePosition(CharacterEntity.Rigidbody.transform.position + direction * (WalkSpeed * Time.deltaTime));
            }

            if (direction != Vector3.zero)
            {
                var transform = CharacterEntity.CharacterInput.transform;
                var toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);
            }
            else{
                CharacterEntity.Rigidbody.Sleep();
            }

            movementMagnitude = direction.magnitude;
            if (CharacterEntity.CharacterMesh.animator)
            {
                CharacterEntity.CharacterMesh.animator.SetFloat("Speed", movementMagnitude);
            }
        }

        public override void FixedUpdate()
        {
            RaycastTest();
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
