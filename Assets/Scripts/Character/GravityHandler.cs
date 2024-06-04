using Character;
using Character.States;
using Character.Utils;
using UnityEngine;

namespace Character
{
    public class GravityHandler : ACharacterMonoBehaviour
    {
        private const float gravityForce = -9.81f;
        private float gravityMultiplyer = 5.0f;
        private float currentYVelocity = 0f;
        private float currentXVelocity = 0f;
        private float currentZVelocity = 0f;
        public bool isGrounded;
        public bool isKnockback;  // Add this flag
        
        public new CharacterEntity CharacterEntity { get; private set; }

        
        public void Update()
        {
            if (isKnockback)
            {
                return;
                //ApplyFallingGravity();
            }

            if (isGrounded)
            {
                currentYVelocity = 0f;
                Debug.Log("Grounded.");
            }
            else
            {
                ApplyGravity();
            }
        }

        public void ApplyGravity()
        {
            currentYVelocity += gravityMultiplyer * gravityForce * Time.deltaTime;
            transform.Translate(new Vector3(0, currentYVelocity, 0) * Time.deltaTime);
        }

        public void ApplyFallingGravity()
        {
            //currentXVelocity CharacterEntity.CharacterState.GetComponent<KnockbackState>().xforKnocbackGravity;
            currentYVelocity += gravityMultiplyer * gravityForce * Time.deltaTime;
            transform.Translate(new Vector3(currentXVelocity, currentYVelocity, currentZVelocity) * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }
    }
}
