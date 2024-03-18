using Character.Utils;

namespace Character.States
{
    public class DispatchHookState : ACharacterState
    {
        public DispatchHookState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.GrapplingHookWeapon.DispatchHook(CharacterEntity.Character.transform.forward);
        }
    }
}
