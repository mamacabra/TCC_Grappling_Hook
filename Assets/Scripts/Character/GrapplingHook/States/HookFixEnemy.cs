using Character.Utils;
using UnityEngine;
using VFX;

namespace Character.GrapplingHook.States
{
    public class HookFixEnemy : AGrapplingHookState
    {
        private readonly Vector3 vfxPoint;

        public HookFixEnemy(CharacterEntity characterEntity, Vector3 enemyPosition) : base(characterEntity)
        {
            vfxPoint = enemyPosition;
        }

        public override void Enter()
        {
            DisableHookCollider();
            VFXManager.Instance.PlayHookHitVFX(vfxPoint);
        }
    }
}
