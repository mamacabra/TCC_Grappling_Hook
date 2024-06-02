using Character;
using Character.Utils;
using UnityEngine;

namespace Character
{
    public class GravityHandler : ACharacterMonoBehaviour
    {
        public float gravityForce = 9.81f; 
        public LayerMask groundLayer; 
        public float groundCheckDistance = 0.1f; 
        private float currentVelocity = 0f;
        private bool isGrounded;


        public new CharacterEntity CharacterEntity { get; private set; }

        public void Update()
        {
            CheckGround();
            if(!isGrounded )
            {
                currentVelocity = -gravityForce * Time.deltaTime;
                CharacterEntity.Rigidbody.MovePosition( currentVelocity*Vector3.down);
            }
            else
            {
                currentVelocity = 0f;
            }
          
        }

        
        public void ApplyGravity()
        {
            if (!isGrounded)
            {
                
 
            }
            else
            {
               
                
            }
        }

        private void CheckGround()
        {
            
            if (Physics.Raycast(transform.position, Vector3.down, out _, groundCheckDistance, groundLayer))
            {
                isGrounded = true;
                Debug.Log("Grounded.");
            }
            else
            {
                isGrounded = false;
                Debug.Log("Not grounded.");
            }
        }
    }
}
