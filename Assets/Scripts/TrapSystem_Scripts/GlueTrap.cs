using UnityEngine;
using TrapSystem_Scripts.ModifierSystem;

namespace TrapSystem_Scripts
{    
    public class GlueTrap : MonoBehaviour {
        public float acceleration;
        public float maxSpeed;

        AModifier glueModifier;

        private void Start() {
            glueModifier = new GlueModifier{ acceleration = this.acceleration, maxSpeed = this.maxSpeed};
        }

        private void OnTriggerEnter(Collider other) {
            if (other.attachedRigidbody.TryGetComponent(out IModifyable modifyable) && other.CompareTag("Character")){
                // if(modifyable is Character.Character) (modifyable as Character.Character).CurrentSpeed = Vector3.zero;
                modifyable.AddModifier(glueModifier);
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.attachedRigidbody.TryGetComponent(out IModifyable modifyable) && other.CompareTag("Character")){
                modifyable.RemoveModifier(glueModifier);
            }
        }
    }
}