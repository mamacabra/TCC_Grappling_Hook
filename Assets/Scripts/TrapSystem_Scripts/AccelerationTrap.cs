using System.Collections;
using System.Collections.Generic;
using TrapSystem_Scripts.ModifierSystem;
using UnityEngine;
using Character;


namespace TrapSystem_Scripts
{
    public class AccelerationTrap : MonoBehaviour
    {
        public float acceleration;
        public float maxSpeed;

        AModifier accelerationModifier;

        private void Start() {
            accelerationModifier = new AccelerationModifier{ acceleration = this.acceleration, maxSpeed = this.maxSpeed};
        }

        private void OnTriggerEnter(Collider other) {
            if (other.attachedRigidbody.TryGetComponent(out IModifyable modifyable)){
                modifyable.AddModifier(accelerationModifier);
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.attachedRigidbody.TryGetComponent(out IModifyable modifyable)){
                modifyable.RemoveModifier(accelerationModifier);
            }
        }
    }
}
