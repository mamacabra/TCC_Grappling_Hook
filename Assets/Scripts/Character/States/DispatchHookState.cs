using Character.Utils;

namespace Character.States
{
    public class DispatchHookState : CharacterState
    {
        private readonly Character _character;

        public DispatchHookState(Character character)
        {
            _character = character;
        }

        public override void Enter()
        {
            _character.grapplingHookWeapon.DispatchHook();
        }
    }
}
