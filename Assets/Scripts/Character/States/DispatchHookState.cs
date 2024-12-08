using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class DispatchHookState : ACharacterState
    {
        private new const float RaycastDistance = 3f;

        public DispatchHookState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            var origin = new Vector3(Transform.position.x, 1f, Transform.position.z);
            var direction = Transform.forward;

            CharacterEntity.Character.EnableHook(false);

            Physics.Raycast(origin, direction, out var hit, RaycastDistance);
            if (hit.collider) CharacterEntity.CharacterState.SetWalkState();
            else {
                CharacterEntity.GrapplingHookState.SetHookCollisionCheckState();
                AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.HookFire);
                AudioManager.audioManager.HookStop();
            }
        }

        public override void Update()
        {
            CharacterEntity.Character.MoveArrowForward();
        }
    }
}
