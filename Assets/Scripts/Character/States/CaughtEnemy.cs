using Character.Utils;

namespace Character.States
{
    public class CaughtEnemy : ACharacterState
    {
        private CharacterEntity enemy;

        public CaughtEnemy(CharacterEntity characterEntity, CharacterEntity enemy) : base(characterEntity)
        {
            this.enemy = enemy;
        }
    }
}
