using UnityEngine;
using UnityEngine.InputSystem;
using Character.GrapplingHook;

namespace Character
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CharacterInput))]
    [RequireComponent(typeof(CharacterState))]
    [RequireComponent(typeof(CharacterUI))]
    [RequireComponent(typeof(GrapplingHookWeapon))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterSetup : MonoBehaviour
    {
        private void Awake()
        {
            var characterController = gameObject.GetComponent<CharacterController>();
            var characterMovement = gameObject.GetComponent<CharacterInput>();
            var characterState = gameObject.GetComponent<CharacterState>();
            var characterUI = gameObject.GetComponent<CharacterUI>();
            var grapplingHookWeapon = gameObject.GetComponent<GrapplingHookWeapon>();

            var entity = new CharacterEntity
            {
                CharacterController = characterController,
                CharacterInput = characterMovement,
                CharacterState = characterState,
                CharacterUI = characterUI,
                GrapplingHookWeapon = grapplingHookWeapon,
            };

            grapplingHookWeapon.Setup(entity);
            characterState.Setup(entity);
            characterMovement.Setup(entity);
        }
    }
}
