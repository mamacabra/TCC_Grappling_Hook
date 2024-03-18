using UnityEngine;

namespace Character.States
{
    public class WalkState : ACharacterState
    {
        private const float MovementSpeed = 18.0f;
        private const float RotationSpeed = 500;

        public WalkState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.GrapplingHookWeapon.ResetHook();
        }

        public override void Update()
        {
            var movementInput = CharacterEntity.CharacterInput.movementInput;
            var direction = new Vector3(movementInput.x, 0, movementInput.y).normalized;

            CharacterEntity.CharacterController.Move(direction * (Time.deltaTime * MovementSpeed));

            if (direction != Vector3.zero)
            {
                var transform = CharacterEntity.CharacterInput.transform;
                var toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * RotationSpeed);
            }
        }
    }
}
