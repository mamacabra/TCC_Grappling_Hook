using Character.Utils;

namespace Character.States
{
    public class WinnerState : ACharacterState
    {
        public WinnerState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.CharacterMesh.animator?.SetBool("isWinner", true);
        }
    }
}
