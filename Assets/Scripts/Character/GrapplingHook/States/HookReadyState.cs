using Character.Utils;

namespace Character.GrapplingHook.States
{
    public class HookReadyState : AGrapplingHookState
    {
        public HookReadyState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Enter()
        {
            CharacterEntity.GrapplingHookCollider.enabled = false;
            CharacterEntity.GrapplingHookTransform.localPosition = GrapplingStats.originPosition;
        }
    }
}
