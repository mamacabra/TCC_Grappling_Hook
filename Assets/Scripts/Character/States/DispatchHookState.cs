using Character.Utils;

namespace Character.States
{
    public class DispatchHookState : ACharacterState
    {
        public DispatchHookState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.Character.UseHook();
            CharacterEntity.GrapplingHookWeapon.DispatchHook(CharacterEntity.CharacterState.transform.forward);
        }
    }
}
