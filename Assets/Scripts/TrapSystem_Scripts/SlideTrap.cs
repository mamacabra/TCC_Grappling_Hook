using System.Collections;
using System.Collections.Generic;
using TrapSystem_Scripts.ModifierSystem;
using UnityEngine;
using Character;
using UnityEngine.Events;


namespace TrapSystem_Scripts
{
    public class SlideTrap : MonoBehaviour
    {
        public float acceleration;
        public float maxSpeed;

        AModifier slideModifier;

        private void Start() {
            slideModifier = new SlideModifier{ acceleration = this.acceleration, maxSpeed = this.maxSpeed};
        }

        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Character")) return;
            if (other.attachedRigidbody.TryGetComponent(out IModifyable modifyable)){
                var characterEntity = other.attachedRigidbody.GetComponent<Character.Character>().CharacterEntity;
                modifyable.AddModifier(characterEntity, slideModifier);
            }
        }

        private void OnTriggerExit(Collider other) {
            if (!other.CompareTag("Character")) return;
            if (other.attachedRigidbody.TryGetComponent(out IModifyable modifyable)){
                var characterEntity = other.attachedRigidbody.GetComponent<Character.Character>().CharacterEntity;
                modifyable.RemoveModifier(characterEntity, slideModifier);
                // if (modifyable is SampleMovement) StartCoroutine(RemoveSpeed(modifyable as SampleMovement));
            }
        }

        // IEnumerator RemoveSpeed(SampleMovement sampleMovement){
        //     sampleMovement.speed = 5f;
        //     yield return new WaitForSeconds(0.25f);
        //     sampleMovement.speed = 20f;
        // }
    }
}
