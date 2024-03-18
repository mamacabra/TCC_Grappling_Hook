using UnityEngine;
using Character.Utils;

namespace Character.States
{
    public class WalkState : ACharacterState
    {
        private readonly CharacterEntity _characterEntity;

        private const float MovementSpeed = 18.0f;
        private const float RotationSpeed = 500;

        public WalkState(CharacterEntity characterEntity)
        {
            _characterEntity = characterEntity;
        }

        public override void Enter()
        {
            _characterEntity.GrapplingHookWeapon.ResetHook();
        }

        public override void Update()
        {
            var movementInput = _characterEntity.CharacterInput.movementInput;
            var direction = new Vector3(movementInput.x, 0, movementInput.y).normalized;

            _characterEntity.CharacterController.Move(direction * (Time.deltaTime * MovementSpeed));

            var transform = _characterEntity.CharacterInput.transform;
            var toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * RotationSpeed);
        }
    }
}
