using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class WalkState : ACharacterState
    {
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
            var directionMagnitude = CharacterEntity.CharacterInput.MoveDirection.magnitude;

            Walk();
            if (directionMagnitude > JoystickDeadZone) LookAt();

            CharacterEntity.Character.MoveArrowForward();
            CharacterEntity.CharacterMesh.animator?.SetBool("isWalking", directionMagnitude > JoystickDeadZone);
        }

        public override void Exit()
        {
            CharacterEntity.CharacterMesh.animator?.SetBool("isWalking", false);
        }
    }
}
