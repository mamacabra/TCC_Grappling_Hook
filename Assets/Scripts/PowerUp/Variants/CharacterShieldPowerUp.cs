using Character;

namespace PowerUp.Variants
{
    public class CharacterShieldPowerUp : APowerUp
    {
        public CharacterShieldPowerUp(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void OnCatch()
        {
            CharacterEntity.Character.ToggleShield(true);
        }
    }
}
