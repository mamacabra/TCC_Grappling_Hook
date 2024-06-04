using Character.Utils;

namespace Character.GrapplingHook.States
{
    public class HookFixEnemy : AGrapplingHookState
    {
        public HookFixEnemy(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            DisableHookCollider();
        }
    }
}
