namespace Character.Utils
{
    public class PlayerColorLayer
    {
        public const int InvalidCharacterId = -1;

        public int CharacterId { get; private set; } = InvalidCharacterId;
        public int ColorLayer { get; }

        public PlayerColorLayer(int colorLayer)
        {
            ColorLayer = colorLayer;
        }

        public void SetCharacterId(int characterId)
        {
            CharacterId = characterId;
        }
    }
}
