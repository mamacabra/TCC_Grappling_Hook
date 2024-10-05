using Character.Utils;

namespace Character.GrapplingHook.States
{
    public class HookCollisionCheckState : AGrapplingHookState
    {
        public HookCollisionCheckState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            switch (CharacterEntity.Hook.Force)
            {
                case 1:
                    CharacterEntity.GrapplingHookColliderCheck.EnableColliderLevel1();
                    break;
                case 2:
                    CharacterEntity.GrapplingHookColliderCheck.EnableColliderLevel1();
                    break;
                default:
                    CharacterEntity.GrapplingHookColliderCheck.EnableColliderLevel1();
                    break;
            }
        }

        public override void Exit()
        {
            CharacterEntity.GrapplingHookColliderCheck.DisableCollider();
        }
    }
}
