using Character.Utils;

namespace Character.States
{
    public class EndGameState : ACharacterState
    {
        public EndGameState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.GrapplingHookState.SetHookReadyState();
        }
    }
}
