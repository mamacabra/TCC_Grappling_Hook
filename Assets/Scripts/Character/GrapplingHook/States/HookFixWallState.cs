using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook.States
{
    public class HookFixWallState : AGrapplingHookState
    {
        private readonly Vector3 wallPoint;

        public HookFixWallState(CharacterEntity characterEntity, Vector3 wallPoint) : base(characterEntity)
        {
            this.wallPoint = wallPoint;
        }

        public override void Enter()
        {
            DisableHookCollider();
        }

        public override void FixedUpdate()
        {
            CharacterEntity.GrapplingHookTransform.position = wallPoint;
        }
    }
}
