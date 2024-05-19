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
    [RequireComponent(typeof(GrapplingHookWeapon))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterSetup : MonoBehaviour
    {
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

            var entity = new CharacterEntity
            {
                Character = character,
                CharacterInput = characterInput,
                CharacterMesh = characterMesh,
                CharacterState = characterState,
                CharacterUI = characterUI,

                AttackMelee = attackMelee,
                GrapplingHookWeapon = grapplingHookWeapon,

                Rigidbody = characterRigidbody,
            };

            character.Setup(entity);
            characterInput.Setup(entity);
            characterMesh.Setup(entity);
            characterState.Setup(entity);
            characterUI.Setup(entity);

            attackMelee.Setup(entity);
            attackMelee.DisableHitbox();
            grapplingHookWeapon.Setup(entity);

            SetupRigidbody(entity);

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
