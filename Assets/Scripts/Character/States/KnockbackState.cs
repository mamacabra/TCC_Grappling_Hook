using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class KnockbackState : ACharacterState
    {
        public KnockbackState(CharacterEntity characterEntity) : base(characterEntity) { }

        private float knockbackForce = 60f;
        private Vector3 knockbackDirection;
        private float knockbackDuration = 0.2f;
        private float knockbackTimer;
        

        public override void Enter()
        {
            knockbackDirection = CharacterEntity.Character.transform.Find("Body").forward;
            knockbackTimer = knockbackDuration;
        }

        public override void Update()
        {
            if(knockbackTimer > 0)
            {
            float knockbackStep=knockbackForce*Time.deltaTime;
            Vector3 knockbackVector = (-knockbackDirection+Vector3.up)*knockbackStep;
            CharacterEntity.Rigidbody.MovePosition(CharacterEntity.Rigidbody.position + knockbackVector);

            knockbackTimer -= Time.deltaTime;
            }
            else
            {
                CharacterEntity.CharacterState.SetWalkState();
            }
        }

    }
}

