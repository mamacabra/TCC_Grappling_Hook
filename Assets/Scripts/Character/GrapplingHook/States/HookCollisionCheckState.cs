using Character.Utils;

namespace Character.GrapplingHook.States
{
    public class HookCollisionCheckState : AGrapplingHookState
    {
        public HookCollisionCheckState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.GrapplingHookColliderCheck.EnableCollider();
        }

        public override void Exit()
        {
            CharacterEntity.GrapplingHookColliderCheck.DisableCollider();
        }
    }
}
