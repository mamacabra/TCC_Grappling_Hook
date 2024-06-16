using UnityEngine;

namespace Character.Utils
{
    public abstract class ACharacterState
    {
        protected readonly CharacterEntity CharacterEntity;
        protected readonly Transform Transform;

        private const float WalkSpeed = 20f;

        private bool hasHitLeft;
        private Color RaycastColorLeft => hasHitLeft ? Color.red : Color.green;
        private bool hasHitCenter;
        private Color RaycastColorCenter => hasHitCenter ? Color.red : Color.green;
        private bool hasHitRight;
        private Color RaycastColorRight => hasHitRight ? Color.red : Color.green;
        protected const float RaycastDistance = 2f;

        protected ACharacterState(CharacterEntity characterEntity)
        {
            CharacterEntity = characterEntity;
            Transform = characterEntity.Character.transform;
        }

        public virtual void Enter() {}
        public virtual void Update() {}
        public virtual void FixedUpdate() {}
        public virtual void Exit() {}

        protected void Walk(float speed = WalkSpeed, bool isDash = false)
        {
            if (CharacterEntity.CharacterMesh.animator)
            {
                var magnitude = CharacterEntity.CharacterInput.MoveDirection.magnitude;
                CharacterEntity.CharacterMesh.animator.SetFloat("Speed", magnitude);
            }

            var origin = new Vector3(Transform.position.x, 1f, Transform.position.z);
            var direction = Vector3.forward * CharacterEntity.CharacterInput.MoveDirection.magnitude;
            if (isDash) direction = Vector3.forward;

            var rayLeftDirection = (Transform.forward + Transform.right * -1).normalized;
            Physics.Raycast(origin, rayLeftDirection, out var hitLeft, RaycastDistance);
            Debug.DrawRay(origin, rayLeftDirection * RaycastDistance, RaycastColorLeft);

            var rayCenterDirection = Transform.forward;
            Physics.Raycast(origin, rayCenterDirection, out var hitCenter, RaycastDistance);
            Debug.DrawRay(origin, rayCenterDirection * RaycastDistance, RaycastColorCenter);

            var rayRightDirection = (Transform.forward + Transform.right).normalized;
            Physics.Raycast(origin, rayRightDirection, out var hitRight, RaycastDistance);
            Debug.DrawRay(origin, rayRightDirection * RaycastDistance, RaycastColorRight);

            //if (hitLeft.collider && hitCenter.collider && hitRight.collider) return;

            if (hitLeft.collider)
            {
                hasHitLeft = hitLeft.collider.CompareTag(Const.Tags.Wall) || hitLeft.collider.CompareTag(Const.Tags.Object);
                if (hasHitLeft) direction += Vector3.right * 0.5f;
            }
            else hasHitLeft = false;

            if (hitCenter.collider)
            {
                hasHitCenter = hitCenter.collider.CompareTag(Const.Tags.Wall) || hitCenter.collider.CompareTag(Const.Tags.Object);
                if (hasHitCenter) direction = new Vector3(direction.x, direction.y, 0);
            }
            else hasHitCenter = false;

            if (hitRight.collider)
            {
                hasHitRight = hitRight.collider.CompareTag(Const.Tags.Wall) || hitRight.collider.CompareTag(Const.Tags.Object);
                if (hasHitRight) direction += Vector3.right * -0.5f;
            }
            else hasHitRight = false;

            direction = direction.normalized;
            Transform.Translate(direction * (speed * Time.deltaTime));
        }

        protected void LookAt()
        {
            var direction = CharacterEntity.CharacterInput.MoveDirection;
            if (direction == Vector3.zero) return;

            var lookDirection = CharacterEntity.CharacterInput.LookDirection;
            CharacterEntity.Character.characterBody.LookAt(lookDirection);
        }
    }
}
