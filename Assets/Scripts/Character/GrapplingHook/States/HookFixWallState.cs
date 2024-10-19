using Character.Utils;
using UnityEditor;
using UnityEngine;

namespace Character.GrapplingHook.States
{
    public class HookFixWallState : AGrapplingHookState
    {
        private readonly Vector3 wallPoint;

        public HookFixWallState(CharacterEntity characterEntity, Vector3 wallPoint) : base(characterEntity)
        {
            var direction = (wallPoint - CharacterEntity.Character.transform.position).normalized;
            var newWallPoint = wallPoint - direction * 1.6f;
            newWallPoint.y = GrapplingStats.originPosition.y;

            this.wallPoint = newWallPoint;
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
