using UnityEngine;
using UnityEngine.InputSystem;
using Character.GrapplingHook;
using Const;

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
        [SerializeField] private bool isDebug;

        private void Awake()
        {
            //var characterController = gameObject.GetComponent<CharacterController>();
            var characterRigidbody = gameObject.GetComponent<Rigidbody>();
            var characterInput = gameObject.GetComponent<CharacterInput>();
            var characterState = gameObject.GetComponent<CharacterState>();
            var characterUI = gameObject.GetComponent<CharacterUI>();
            var grapplingHookWeapon = gameObject.GetComponent<GrapplingHookWeapon>();

            var entity = new CharacterEntity
            {
                IsDebug = isDebug,
                CharacterRigidbody = characterRigidbody,
                CharacterInput = characterInput,
                CharacterState = characterState,
                CharacterUI = characterUI,
                GrapplingHookWeapon = grapplingHookWeapon,
            };

            characterInput.Setup(entity);
            characterState.Setup(entity);
            characterUI.Setup(entity);
            grapplingHookWeapon.Setup(entity);

            gameObject.tag = Tags.Character;

            if (isDebug == false) characterState.SetWalkState();
        }
    }
}
