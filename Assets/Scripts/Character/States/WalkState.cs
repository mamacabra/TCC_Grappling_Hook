using Character.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace Character.States
{
    public class WalkState : ACharacterState
    {
        private const float MovementSpeed = 30f;
        private const float RotationSpeed = 1000f;

        public WalkState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.GrapplingHookWeapon.ResetHook();
        }

        public override void Update()
        {
            var movementInput = CharacterEntity.CharacterInput.movementInput;
            var direction = new Vector3(movementInput.x, 0, movementInput.y);


            CharacterEntity.CharacterRigidbody.MovePosition(CharacterEntity.CharacterRigidbody.transform.position+direction*Time.deltaTime*MovementSpeed);

            if (direction != Vector3.zero)
            {
                var transform = CharacterEntity.CharacterInput.transform;
                var toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * RotationSpeed);
            }
        }
    }
}
