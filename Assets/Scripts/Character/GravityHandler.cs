using Character;
using Character.Utils;
using UnityEngine;

namespace Character
{
    public class GravityHandler : ACharacterMonoBehaviour
    {
        private const float gravityForce = -9.81f;
        private float gravityMultiplyer = 5.0f;
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
                ApplyGravity();
            }
           
          
        }

        
        public void ApplyGravity()
        {
            currentVelocity += gravityForce * gravityMultiplyer * Time.deltaTime;
            transform.Translate(new Vector3(transform.position.x, currentVelocity, transform.position.z)*Time.deltaTime);
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
