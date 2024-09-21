using Character;

namespace PowerUp.Variants
{
    public class CharacterSpeedBoostPowerUp : APowerUp
    {
        public CharacterSpeedBoostPowerUp(CharacterEntity entity) : base(entity) {}

        public override void OnCatch()
        {
            CharacterEntity.Character.ToggleSpeedBoost(true);
        }

        public override void OnDrop()
        {
            CharacterEntity.Character.ToggleSpeedBoost(false);
        }
    }
}
