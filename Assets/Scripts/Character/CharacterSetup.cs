using UnityEngine;
using UnityEngine.InputSystem;
using Character.GrapplingHook;
using Character.Melee;
using Const;
using UnityEngine.Serialization;

namespace Character
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(CharacterInput))]
    [RequireComponent(typeof(CharacterMesh))]
    [RequireComponent(typeof(CharacterState))]
    [RequireComponent(typeof(CharacterUI))]
    [RequireComponent(typeof(GrapplingHookWeapon))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterSetup : MonoBehaviour
    {
        [SerializeField] private GrapplingHookState grapplingHookState;
        [SerializeField] private GrapplingHookWeapon grapplingWeapon;

        private void Awake()
        {
            gameObject.tag = Tags.Character;

            var character = gameObject.GetComponent<Character>();
            var characterInput = gameObject.GetComponent<CharacterInput>();
            var characterMesh = gameObject.GetComponent<CharacterMesh>();
            var characterRigidbody = gameObject.GetComponent<Rigidbody>();
            var characterState = gameObject.GetComponent<CharacterState>();
            var characterUI = gameObject.GetComponent<CharacterUI>();

            var attackMelee = gameObject.transform.Find("Body/AttackMelee").GetComponent<AttackMelee>();
            var grapplingHookWeapon = gameObject.GetComponent<GrapplingHookWeapon>();

            var characterEntity = new CharacterEntity
            {
                Character = character,
                CharacterInput = characterInput,
                CharacterMesh = characterMesh,
                CharacterState = characterState,
                CharacterUI = characterUI,

                AttackMelee = attackMelee,
                GrapplingHookState = grapplingHookState,
                GrapplingHookWeapon = grapplingHookWeapon,

                Rigidbody = characterRigidbody,
            };

            character.Setup(characterEntity);
            characterInput.Setup(characterEntity);
            characterMesh.Setup(characterEntity);
            characterState.Setup(characterEntity);
            characterUI.Setup(characterEntity);

            attackMelee.Setup(characterEntity);
            attackMelee.DisableHitbox();
            grapplingHookWeapon.Setup(characterEntity);

            SetupRigidbody(characterEntity);

            characterState.SetWalkState();
        }

        private static void SetupRigidbody(CharacterEntity entity)
        {
            entity.Rigidbody.useGravity = false;
            entity.Rigidbody.isKinematic = true;
            entity.Rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            entity.Rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }
}
