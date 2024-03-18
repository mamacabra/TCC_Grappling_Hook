using UnityEngine;
using UnityEngine.InputSystem;
using Character.GrapplingHook;

namespace Character
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CharacterInput))]
    [RequireComponent(typeof(CharacterUI))]
    [RequireComponent(typeof(GrapplingHookWeapon))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterSetup : MonoBehaviour
    {
        private void Awake()
        {
            var character = gameObject.GetComponent<Character>();
            var characterController = gameObject.GetComponent<CharacterController>();
            var characterMovement = gameObject.GetComponent<CharacterInput>();
            var characterRigidbody = gameObject.GetComponent<Rigidbody>();
            var characterUI = gameObject.GetComponent<CharacterUI>();
            var grapplingHookWeapon = gameObject.GetComponent<GrapplingHookWeapon>();

            var entity = new CharacterEntity
            {
                Character = character,
                CharacterController = characterController,
                CharacterInput = characterMovement,
                CharacterRigidbody = characterRigidbody,
                CharacterUI = characterUI,
                GrapplingHookWeapon = grapplingHookWeapon,
            };

            grapplingHookWeapon.Setup(entity);
            character.Setup(entity);
            characterMovement.Setup(entity);
        }
    }
}
