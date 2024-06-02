using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class HookedToWallState : ACharacterState
    {
        private const float MinDistanceToDrop = 3f;
        private const float MovementSpeed = 50f;
        private readonly Vector3 _wallPoint;

        public HookedToWallState(CharacterEntity characterEntity, Vector3 wallPoint) : base(characterEntity)
        {
            _wallPoint = wallPoint;
        }

        public override void Enter()
        {
            // CharacterEntity.GrapplingHookWeapon.AttachHook();
        }

        public override void FixedUpdate()
        {
            var characterPosition = Transform.position;
            var direction = (_wallPoint - characterPosition).normalized;

            CharacterEntity.Rigidbody.MovePosition(characterPosition + direction * (Time.fixedDeltaTime * MovementSpeed));

            var distance = Vector3.Distance(characterPosition, _wallPoint);
            if (distance <= MinDistanceToDrop)
                CharacterEntity.CharacterState.SetWalkState();
        }
    }
}
