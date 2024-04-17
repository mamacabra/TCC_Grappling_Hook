using UnityEngine;
using UnityEngine.InputSystem;
using Character.GrapplingHook;
using Const;

namespace Character
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(CharacterInput))]
    [RequireComponent(typeof(CharacterMesh))]
    [RequireComponent(typeof(CharacterRaycast))]
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
            var character = gameObject.GetComponent<Character>();
            var characterInput = gameObject.GetComponent<CharacterInput>();
            var characterMesh = gameObject.GetComponent<CharacterMesh>();
            var characterRaycast = gameObject.GetComponent<CharacterRaycast>();
            var characterRigidbody = gameObject.GetComponent<Rigidbody>();
            var characterState = gameObject.GetComponent<CharacterState>();
            var characterUI = gameObject.GetComponent<CharacterUI>();
            var grapplingHookWeapon = gameObject.GetComponent<GrapplingHookWeapon>();

            var entity = new CharacterEntity
            {
                IsDebug = isDebug,
                Character = character,
                CharacterInput = characterInput,
                CharacterMesh = characterMesh,
                CharacterRaycast = characterRaycast,
                CharacterState = characterState,
                CharacterUI = characterUI,
                GrapplingHookWeapon = grapplingHookWeapon,
                Rigidbody = characterRigidbody,
            };

            character.Setup(entity);
            characterInput.Setup(entity);
            characterMesh.Setup(entity);
            characterState.Setup(entity);
            characterUI.Setup(entity);
            grapplingHookWeapon.Setup(entity);

            SetupRigidbody(entity);

            gameObject.tag = Tags.Character;

            if (isDebug == false) characterState.SetWalkState();
        }

        private static void SetupRigidbody(CharacterEntity entity)
        {
            entity.Rigidbody.useGravity = false;
            entity.Rigidbody.isKinematic = false;
            entity.Rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            entity.Rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }
}
