using Character.Utils;
using UnityEngine;
using VFX;

namespace Character.GrapplingHook.States
{
    public class HookFixWallState : AGrapplingHookState
    {
        private readonly Vector3 vfxPoint;
        private const float VFXPointRecoil = 0.5f;
        private readonly Vector3 hookPoint;
        private const float HookPointRecoil = 1.5f;

        public HookFixWallState(CharacterEntity characterEntity, Vector3 wallPoint) : base(characterEntity)
        {
            var direction = (wallPoint - CharacterEntity.Character.transform.position).normalized;

            vfxPoint = wallPoint - direction * VFXPointRecoil;

            var newWallPoint = wallPoint - direction * HookPointRecoil;
            newWallPoint.y = GrapplingStats.OriginPosition.y;
            hookPoint = newWallPoint;
        }

        public override void Enter()
        {
            DisableHookCollider();
            VFXManager.Instance.PlayHookHitVFX(vfxPoint);
        }

        public override void FixedUpdate()
        {
            CharacterEntity.GrapplingHookTransform.position = hookPoint;
        }
    }
}
