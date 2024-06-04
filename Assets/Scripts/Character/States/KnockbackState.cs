using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class KnockbackState : ACharacterState
    {
        private float knockbackForce = 60.0f;
        private float upwardForce = 45f;
        private Vector3 knockbackDirection;
        private float knockbackDuration = 0.3f;
        private float knockbackTimer;
        public Vector3 xforKnocbackGravity;
        public Vector3 zforKnocbackGravity;

        public KnockbackState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Enter()
        {
            knockbackDirection = CharacterEntity.Character.transform.Find("Body").forward;
            knockbackTimer = knockbackDuration;

            
            //CharacterEntity.Character.GetComponent<GravityHandler>().isKnockback = true;
        }

        public override void Update()
        {
            if (knockbackTimer > 0)
            {
                float knockbackStep = knockbackForce * Time.deltaTime;
                float upwardStep = upwardForce * Time.deltaTime;

                Vector3 knockbackVector = (-knockbackDirection * knockbackStep)/* + (Vector3.up * upwardStep)*/;
                Vector3 newPosition = CharacterEntity.Rigidbody.position + knockbackVector;
                //xforKnocbackGravity = new Vector3(-knockbackDirection.x, 0, 0);
                //zforKnocbackGravity = new Vector3(0, 0, -knockbackDirection.z);

                CharacterEntity.Rigidbody.MovePosition(newPosition);

                knockbackTimer -= Time.deltaTime;
            }
            else
            {
                CharacterEntity.Character.GetComponent<GravityHandler>().isKnockback = false;
                CharacterEntity.CharacterState.SetWalkState();
            }
        }
    }
}
