using Character.Utils;

namespace Character.GrapplingHook.States
{
    public class HookDestroyedState : AGrapplingHookState
    {
        public HookDestroyedState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            DisableHookCollider();
        }
    }
}
