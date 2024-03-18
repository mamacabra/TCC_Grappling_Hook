using Character.Utils;

namespace Character.States
{
    public class RollbackHookState : ACharacterState
    {
        public RollbackHookState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.GrapplingHookWeapon.RollbackHook();
        }
    }
}
