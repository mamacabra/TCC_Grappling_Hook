using Character.Utils;

namespace Character.States
{
    public class DispatchHookState : ACharacterState
    {
        private readonly CharacterEntity _characterEntity;

        public DispatchHookState(CharacterEntity characterEntity)
        {
            _characterEntity = characterEntity;
        }

        public override void Enter()
        {
            _characterEntity.GrapplingHookWeapon.DispatchHook(_characterEntity.Character.transform.forward);
        }
    }
}
