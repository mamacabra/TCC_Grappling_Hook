using Character.Utils;

namespace Character.GrapplingHook.States
{
    public class HookReadyState : AGrapplingHookState
    {
        public HookReadyState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            DisableHookCollider();
            CharacterEntity.GrapplingHookTransform.localPosition = GrapplingStats.originPosition;
        }
    }
}
