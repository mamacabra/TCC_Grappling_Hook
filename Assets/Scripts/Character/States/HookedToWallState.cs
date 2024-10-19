using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class HookedToWallState : ACharacterState
    {
        private const float MinDistanceToDrop = 5f;
        private const float MovementSpeed = 50f;
        private readonly Vector3 wallPoint;

        public HookedToWallState(CharacterEntity characterEntity, Vector3 wallPoint) : base(characterEntity)
        {
            this.wallPoint = wallPoint;
        }

        public override void Enter()
        {
            CharacterEntity.GrapplingHookState.SetHookFixWallState(wallPoint);
            AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.HookHitWall);
        }

        public override void FixedUpdate()
        {
            var characterPosition = Transform.position;
            var direction = (wallPoint - characterPosition).normalized;

            CharacterEntity.Rigidbody.MovePosition(characterPosition + direction * (Time.fixedDeltaTime * MovementSpeed));

            var distance = Vector3.Distance(characterPosition, wallPoint);
            if (distance <= MinDistanceToDrop)
                CharacterEntity.CharacterState.SetWalkState();

            CharacterEntity.Character.MoveArrowForward();
        }
    }
}
