using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class KnockbackState : ACharacterState
    {
        private const float KnockbackForce = 50.0f;
        private const float KnockbackDuration = 0.2f;

        private float knockbackTimer;
        private Vector3 knockbackDirection;

        public KnockbackState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.CharacterVFX.PlayParryVFX();
            knockbackDirection = CharacterEntity.Character.transform.Find("Body").forward;
            knockbackTimer = KnockbackDuration;
        }

        public override void Update()
        {
            if (knockbackTimer > 0)
            {
                var knockbackStep = KnockbackForce * Time.fixedDeltaTime;
                var knockbackVector = (-knockbackDirection * knockbackStep);
                var newPosition = CharacterEntity.Rigidbody.position + knockbackVector;

                var rayDirection = (-Transform.forward).normalized;
                var rayOrigin = new Vector3(Transform.position.x, 1f, Transform.position.z);
                Physics.Raycast(rayOrigin, rayDirection, out var hit, RaycastDistance);

                if (hit.collider)
                    newPosition = CharacterEntity.Rigidbody.position - knockbackVector;

                CharacterEntity.Rigidbody.MovePosition(newPosition);
                knockbackTimer -= Time.deltaTime;
            }
            else
            {
                CharacterEntity.CharacterState.SetWalkState();
            }
        }
    }
}
