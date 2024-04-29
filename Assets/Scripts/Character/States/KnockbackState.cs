using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class KnockbackState : ACharacterState
    {
        public KnockbackState(CharacterEntity characterEntity) : base(characterEntity) { }

        private float knockbackForce = 100f;
        private Vector3 knockbackDirection;

        public override void Update()
        {
            knockbackDirection = CharacterEntity.CharacterInput.transform.forward;
            CharacterEntity.Rigidbody.AddForce(-knockbackDirection * knockbackForce, ForceMode.Impulse);
            
            
        }


    }
}

