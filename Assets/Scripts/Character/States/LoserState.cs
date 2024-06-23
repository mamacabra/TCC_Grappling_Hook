using Character.Utils;

namespace Character.States
{
    public class LoserState : ACharacterState
    {
        public LoserState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.GrapplingHookState.SetHookReadyState();
        }
    }
}
