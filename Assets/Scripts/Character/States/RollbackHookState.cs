using Character.Utils;

namespace Character.States
{
    public class RollbackHookState : CharacterState
    {
        private readonly CharacterEntity _characterEntity;

        public RollbackHookState(CharacterEntity characterEntity)
        {
            _characterEntity = characterEntity;
        }

        public override void Enter()
        {
            _characterEntity.GrapplingHookWeapon.RollbackHook();
        }
    }
}
