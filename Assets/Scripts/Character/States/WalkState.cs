using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class WalkState : CharacterState
    {
        private const float Speed = 5.0f;
        private readonly CharacterEntity _characterEntity;

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
            Vector2 movementInput = _characterEntity.CharacterMovement.movementInput;
            Vector3 moveDir = new Vector3(movementInput.x, 0, movementInput.y);
            Vector3 movePos = new Vector3(movementInput.x, Physics.gravity.y, movementInput.y);
            moveDir.Normalize();

            _characterEntity.CharacterController.Move(movePos * (Time.deltaTime * _characterEntity.CharacterMovement.speed));

            if (moveDir != Vector3.zero)
            {
                var transform = _characterEntity.CharacterMovement.transform;
                Quaternion toRotation = Quaternion.LookRotation(moveDir);
                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * _characterEntity.CharacterMovement.rotationSpeed);
            }
        }
    }
}
