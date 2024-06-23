using Character.Utils;

namespace Character.States
{
    public class ReadyState : ACharacterState
    {
        public ReadyState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            // TODO: Reproduzir animação de idle
        }
    }
}
