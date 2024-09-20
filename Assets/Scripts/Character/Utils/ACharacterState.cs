using UnityEngine;

using TrapSystem_Scripts.ModifierSystem;

namespace Character.Utils
{
    public abstract class ACharacterState
    {
        protected readonly CharacterEntity CharacterEntity;
        protected readonly Transform Transform;

        private const float WalkSpeed = 20f;
        private const float WalkAcceleration = 500f;
        Vector3 targetSpeed;
        float acceleration;

        private bool hasHitLeft;
        private Color RaycastColorLeft => hasHitLeft ? Color.red : Color.green;
        private bool hasHitCenter;
        private Color RaycastColorCenter => hasHitCenter ? Color.red : Color.green;
        private bool hasHitRight;
        private Color RaycastColorRight => hasHitRight ? Color.red : Color.green;
        protected const float RaycastDistance = 2f;
        private bool hasHit;

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
            var direction = CharacterEntity.CharacterInput.MoveDirection;
            if (isDash) direction = Transform.forward; // It allows dash when stoped.

            var rayLeftDirection = (Transform.forward + Transform.right * -1).normalized;
            Physics.Raycast(origin, rayLeftDirection, out var hitLeft, RaycastDistance);
            Debug.DrawRay(origin, rayLeftDirection * RaycastDistance, RaycastColorLeft);

            var rayCenterDirection = Transform.forward;
            Physics.Raycast(origin, rayCenterDirection, out var hitCenter, RaycastDistance);
            Debug.DrawRay(origin, rayCenterDirection * RaycastDistance, RaycastColorCenter);

            var rayRightDirection = (Transform.forward + Transform.right).normalized;
            Physics.Raycast(origin, rayRightDirection, out var hitRight, RaycastDistance);
            Debug.DrawRay(origin, rayRightDirection * RaycastDistance, RaycastColorRight);

            var boxCenterDirection = Transform.forward;
            var colliders = new Collider[10];
            Physics.OverlapBoxNonAlloc(origin, Vector3.one, colliders, Quaternion.identity);

            foreach (var collider in colliders)
            {
                if (collider && collider.gameObject != CharacterEntity.Character.gameObject) {
                    hasHit = collider.CompareTag(Const.Tags.Wall) || collider.CompareTag(Const.Tags.Object) || collider.CompareTag(Const.Tags.Character);
                    if (hasHit) direction += Transform.forward * -0.5f;
                }
                else hasHit = false;
            }

            if (hitLeft.collider)
            {
                hasHitLeft = hitLeft.collider.CompareTag(Const.Tags.Wall) || hitLeft.collider.CompareTag(Const.Tags.Object) || hitLeft.collider.CompareTag(Const.Tags.Character);
                if (hasHitLeft) direction += Transform.right * 0.5f;
            }
            else hasHitLeft = false;

            if (hitCenter.collider)
            {
                hasHitCenter = hitCenter.collider.CompareTag(Const.Tags.Wall) || hitCenter.collider.CompareTag(Const.Tags.Object) || hitCenter.collider.CompareTag(Const.Tags.Character);
                if (hasHitCenter) direction -= direction;
            }
            else hasHitCenter = false;

            if (hitRight.collider)
            {
                hasHitRight = hitRight.collider.CompareTag(Const.Tags.Wall) || hitRight.collider.CompareTag(Const.Tags.Object) || hitRight.collider.CompareTag(Const.Tags.Character);
                if (hasHitRight) direction += Transform.right * -0.5f;
            }
            else hasHitRight = false;

            direction = direction.normalized;
            targetSpeed = direction * speed;
            acceleration = WalkAcceleration * Time.deltaTime;

            bool hasSlow = false;

            foreach (var modifier in CharacterEntity.Character.Modifiers) {
                if (modifier is MovementModifier) modifier.ApplyModifier(ref targetSpeed, ref acceleration, direction);
                hasSlow = modifier is GlueModifier;
            }

            if (isDash) {
                CharacterEntity.Character.CurrentSpeed = direction * speed;
                targetSpeed = direction * speed;
                acceleration = WalkAcceleration * Time.deltaTime;
            }

            if (hasSlow && CharacterEntity.Character.CurrentSpeed.magnitude > WalkSpeed){
                CharacterEntity.Character.CurrentSpeed *= 0.5f;
                acceleration = (WalkAcceleration + WalkAcceleration * 0.5f) * Time.deltaTime;
            }

            CharacterEntity.Character.CurrentSpeed = Vector3.MoveTowards(CharacterEntity.Character.CurrentSpeed, targetSpeed, acceleration);

            Transform.Translate(CharacterEntity.Character.CurrentSpeed * Time.deltaTime, Space.World);
        }

        protected void LookAt()
        {
            var direction = CharacterEntity.CharacterInput.MoveDirection.normalized;
            if (direction == Vector3.zero) return;

            var targetRotation = Quaternion.LookRotation(direction);
            Transform.rotation = Quaternion.Slerp(Transform.rotation , targetRotation, Time.deltaTime * 35f);
        }
    }
}
