using Character.Utils;

namespace Character.States
{
    public class RollbackHookState : CharacterState
    {
        private readonly Character _character;

        public RollbackHookState(Character character)
        {
            _character = character;
        }

        public override void Enter()
        {
            _character.CharacterEntity.GrapplingHookWeapon.RollbackHook();
        }
    }
}
