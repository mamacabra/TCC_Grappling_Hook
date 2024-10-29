using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class WalkState : ACharacterState
    {
        private const float JoystickDeadZone = 0.2f;

        public WalkState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.GrapplingHookState.SetHookReadyState();

            var newPosition = CharacterEntity.Character.transform.position;
            newPosition.y = 0.5f;
            CharacterEntity.Character.transform.position = newPosition;
        }

        public override void FixedUpdate()
        {
            Walk();
            LookAt();
            CharacterEntity.Character.MoveArrowForward();

            var directionMagnitude = CharacterEntity.CharacterInput.MoveDirection.magnitude;
            CharacterEntity.CharacterMesh.animator?.SetBool("isWalking", directionMagnitude > JoystickDeadZone);
        }

        public override void Exit()
        {
            CharacterEntity.CharacterMesh.animator?.SetBool("isWalking", false);
        }
    }
}
