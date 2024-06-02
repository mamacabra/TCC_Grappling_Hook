using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class KnockbackState : ACharacterState
    {
        private float knockbackForce = 45f;
        private float upwardForce = 15f; 
        private Vector3 knockbackDirection;
        private float knockbackDuration = 0.3f;
        private float knockbackTimer;

        public KnockbackState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Enter()
        {
            
            knockbackDirection = CharacterEntity.Character.transform.Find("Body").forward;
            knockbackTimer = knockbackDuration;

      
        }

        public override void Update()
        {
            if (knockbackTimer > 0)
            {
                float knockbackStep = knockbackForce * Time.deltaTime;
                float upwardStep = upwardForce * Time.deltaTime;
                
                Vector3 knockbackVector = (-knockbackDirection * knockbackStep) + (Vector3.up * upwardStep);                
                Vector3 newPosition = CharacterEntity.Rigidbody.position + knockbackVector;

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
