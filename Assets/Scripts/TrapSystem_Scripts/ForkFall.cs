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
            var character = other.GetComponent<Character.Character>();
            Debug.Log("HITTED");
            if (other.CompareTag("Character") == false) return;
            if (character.CharacterEntity.CharacterState.State is DeathState) return;
            
            if (other.CompareTag("Character"))
            {
               character.CharacterEntity.CharacterState.SetDeathState(character.CharacterEntity.Character.characterBody);
               PlayersManager.Instance.PlayersToSendToCamera(other.transform, false);
               PlayersManager.Instance.RemovePointsToPlayer(character.Id);
            }
        }
    }
}
