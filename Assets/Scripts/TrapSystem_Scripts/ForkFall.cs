using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrapSystem_Scripts
{
    public class ForkFall : MonoBehaviour
    {
        public float fallSpeed = 20f;

        void Update()
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

            if (transform.position.y <= 0) // Assuming ground level is at y = 0
            {
                Destroy(gameObject);  // Destroy the object if it reaches the ground
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // Implement player death logic here
                Destroy(collision.gameObject);  // Example: destroy player on contact
            }
        }
    }
}
