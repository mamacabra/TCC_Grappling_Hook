using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class ParryAttackState : ACharacterState
    {
        private const float ParryForce = 50.0f;
        private const float ParryDuration = 0.2f;

        private float parryTimer;
        private Vector3 parryDirection;

        public ParryAttackState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            parryDirection = CharacterEntity.Character.transform.Find("Body").forward;
            parryTimer = ParryDuration;
            CharacterEntity.CharacterMesh.animator?.SetBool("isParry", true);
        }

        public override void FixedUpdate()
        {
            if (parryTimer > 0)
            {
                var knockbackStep = ParryForce * Time.fixedDeltaTime;
                var knockbackVector = (-parryDirection * knockbackStep);
                var newPosition = CharacterEntity.Rigidbody.position + knockbackVector;

                var rayDirection = (-Transform.forward).normalized;
                var rayOrigin = new Vector3(Transform.position.x, 1f, Transform.position.z);
                Physics.Raycast(rayOrigin, rayDirection, out var hit, RaycastDistance);

                if (hit.collider)
                    newPosition = CharacterEntity.Rigidbody.position - knockbackVector;

                CharacterEntity.Rigidbody.MovePosition(newPosition);
                parryTimer -= Time.deltaTime;
            }
            else
            {
                CharacterEntity.CharacterState.SetWalkState();
            }
        }

        public override void Exit()
        {
            CharacterEntity.CharacterMesh.animator?.SetBool("isParry", false);
        }
    }
}
