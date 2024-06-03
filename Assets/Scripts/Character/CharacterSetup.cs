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
    [RequireComponent(typeof(CharacterState))]
    [RequireComponent(typeof(CharacterUI))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterSetup : MonoBehaviour
    {
        [Header("Grappling Hook")]
        [SerializeField] private GrapplingHook.GrapplingHook grapplingHook;
        [SerializeField] private GameObject grapplingHookRope;
        [SerializeField] private GameObject grapplingHookRopeMuzzle;
        [SerializeField] private BoxCollider grapplingHookCollider;
        [SerializeField] private GrapplingHookState grapplingHookState;
        [SerializeField] private Transform grapplingHookTransform;

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

            var characterEntity = new CharacterEntity
            {
                Character = character,
                CharacterInput = characterInput,
                CharacterMesh = characterMesh,
                CharacterState = characterState,
                CharacterUI = characterUI,

                AttackMelee = attackMelee,

                GrapplingHookRope = grapplingHookRope,
                GrapplingHookRopeMuzzle = grapplingHookRopeMuzzle,
                GrapplingHookCollider = grapplingHookCollider,
                GrapplingHookState = grapplingHookState,
                GrapplingHookTransform = grapplingHookTransform,

                Rigidbody = characterRigidbody,
            };

            character.Setup(characterEntity);
            characterInput.Setup(characterEntity);
            characterMesh.Setup(characterEntity);
            characterState.Setup(characterEntity);
            characterUI.Setup(characterEntity);

            attackMelee.Setup(characterEntity);
            attackMelee.DisableHitbox();

            grapplingHook?.Setup(characterEntity);
            grapplingHookState?.Setup(characterEntity);

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
