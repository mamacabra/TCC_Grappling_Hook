using UnityEngine;

namespace Character.Utils
{
    public class PlayerColorLayer
    {
        public const int InvalidCharacterId = -1;

        public int CharacterId { get; private set; } = InvalidCharacterId;
        public Color ColorBase { get; }
        public int ColorLayer { get; }

        public PlayerColorLayer(int colorLayer)
        {
            ColorLayer = colorLayer;
        }

        public PlayerColorLayer(int colorLayer, Color colorBase)
        {
            ColorLayer = colorLayer;
            ColorBase = colorBase;
        }

        public void SetCharacterId(int characterId)
        {
            CharacterId = characterId;
        }
    }
}
