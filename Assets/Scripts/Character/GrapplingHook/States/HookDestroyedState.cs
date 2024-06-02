using Character.Utils;

namespace Character.GrapplingHook.States
{
    public class HookDestroyedState : AGrapplingHookState
    {
        public HookDestroyedState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Enter()
        {
            // hook.gameObject.SetActive(false);
            CharacterEntity.GrapplingHookRope.SetActive(false);
            CharacterEntity.GrapplingHookRopeMuzzle.SetActive(false);
        }
    }
}
