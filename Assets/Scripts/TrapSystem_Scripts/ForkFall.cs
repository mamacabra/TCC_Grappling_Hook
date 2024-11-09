using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.States;

namespace TrapSystem_Scripts
{
    public class ForkFall : MonoBehaviour
    {
        public float fallSpeed = 20f;
        private bool canDestroyFork = false;
        private void Start()
        {
            Debug.Log("set tag wall");
            AudioManager.audioManager.PlayLevelSoundEffect(LevelSoundsList.LevelForkTrap);
            
        }
        
        private void Update()
        {
            transform.Translate(Vector3.down * (fallSpeed * Time.deltaTime));

            if (transform.position.y <= 1f)
            {
                fallSpeed = 0;
                transform.position = new Vector3(transform.position.x, 13f, transform.position.z);
                SetTag();
            }

            if (PlayersManager.Instance.GameOver)
            {
                canDestroyFork = true;
            }
            else
            {
                canDestroyFork = false;
            }

            if (canDestroyFork)
            {
                DestroyFork();
            }
                
            
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if(PlayersManager.Instance.GameOver) return;
            var character = other.GetComponent<Character.Character>();
            Debug.Log("HITTED");
            if (other.CompareTag("Character") == false) return;
            if (character.CharacterEntity.CharacterState.State is DeathState) return;
            
            if (other.CompareTag("Character"))
            {
                if(gameObject.CompareTag("Wall"))return;
               character.CharacterEntity.CharacterState.SetDeathState(character.CharacterEntity.Character.characterBody);
               PlayersManager.Instance.PlayersToSendToCamera(other.transform, false);
               PlayersManager.Instance.RemovePointsToPlayer(character.Id);
               fallSpeed = 0;
               transform.position = new Vector3(transform.position.x, 13f, transform.position.z);
               SetTag();
            }
        }

        private void SetTag()
        {
            gameObject.tag = "Wall";
        }

        public void DestroyFork()
        {
            Destroy(gameObject);
        }
    }
}
