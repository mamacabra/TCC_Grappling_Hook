using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.States;

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Character") == false) return;
            if (other.GetComponent<Character.CharacterState>() is DeathState) return;
            
            if (other.CompareTag("Character"))
            {
                other.GetComponent<Character.CharacterEntity>().CharacterState.SetDeathState(other.transform);
            }
        }
    }
}
