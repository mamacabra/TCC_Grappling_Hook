using UnityEngine;
using UnityEngine.InputSystem;
using Character.GrapplingHook;
using Character.Melee;
using Const;

namespace Character
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(CharacterInput))]
    [RequireComponent(typeof(CharacterMesh))]
    [RequireComponent(typeof(CharacterPowerUp))]
    [RequireComponent(typeof(CharacterState))]
    [RequireComponent(typeof(CharacterUI))]
    [RequireComponent(typeof(CharacterVFX))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterSetup : MonoBehaviour
    {
        [Header("Grappling Hook")]
        [SerializeField] private GrapplingHook.GrapplingHook grapplingHook;
        [SerializeField] private GameObject grapplingHookRope;
        [SerializeField] private GameObject grapplingHookRopeMuzzle;
        [SerializeField] private BoxCollider grapplingHookCollider;
        [SerializeField] private GrapplingHookColliderCheck grapplingHookColliderCheck;
        [SerializeField] private GrapplingHookState grapplingHookState;
        [SerializeField] private Transform grapplingHookTransform;

        private void Awake()
        {
            gameObject.tag = Tags.Character;

            var character = gameObject.GetComponent<Character>();
            var characterCollider = gameObject.GetComponent<BoxCollider>();
            var characterInput = gameObject.GetComponent<CharacterInput>();
            var characterMesh = gameObject.GetComponent<CharacterMesh>();
            var characterPowerUp = gameObject.GetComponent<CharacterPowerUp>();
            var characterRigidbody = gameObject.GetComponent<Rigidbody>();
            var characterState = gameObject.GetComponent<CharacterState>();
            var characterUI = gameObject.GetComponent<CharacterUI>();
            var characterVFX = gameObject.GetComponent<CharacterVFX>();
            var gamepadVibrate = gameObject.GetComponent<GamepadVibrate>();

            var attackMelee = gameObject.transform.Find("Body/AttackMelee").GetComponent<AttackMelee>();

            var characterEntity = new CharacterEntity
            {
                Character = character,
                CharacterCollider = characterCollider,
                CharacterInput = characterInput,
                CharacterMesh = characterMesh,
                CharacterPowerUp = characterPowerUp,
                CharacterState = characterState,
                CharacterUI = characterUI,
                CharacterVFX = characterVFX,
                GamepadVibrate = gamepadVibrate,

                AttackMelee = attackMelee,

                Hook = grapplingHook,
                GrapplingHookRope = grapplingHookRope,
                GrapplingHookRopeMuzzle = grapplingHookRopeMuzzle,
                GrapplingHookCollider = grapplingHookCollider,
                GrapplingHookColliderCheck = grapplingHookColliderCheck,
                GrapplingHookState = grapplingHookState,
                GrapplingHookTransform = grapplingHookTransform,

                Rigidbody = characterRigidbody,
            };

            character.Setup(characterEntity);
            characterInput.Setup(characterEntity);
            characterMesh.Setup(characterEntity);
            characterPowerUp.Setup(characterEntity);
            characterState.Setup(characterEntity);
            characterUI.Setup(characterEntity);
            characterVFX.Setup(characterEntity);

            attackMelee.Setup(characterEntity);
            attackMelee.DisableHitbox();

            grapplingHook?.Setup(characterEntity);
            grapplingHookState?.Setup(characterEntity);
            grapplingHookColliderCheck?.Setup(characterEntity);

            SetupRigidbody(characterEntity);
            characterState.SetReadyState();
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
