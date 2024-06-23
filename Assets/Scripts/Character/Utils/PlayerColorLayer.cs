using Const;
using UnityEngine;

namespace Character.Utils
{
    public class PlayerColorLayer
    {
        public const int InvalidCharacterId = -1;

        public int CharacterId { get; private set; } = InvalidCharacterId;
        public Color ColorBase { get; }
        public ControlColorsLayer ColorsLayer { get; }

        public PlayerColorLayer(ControlColorsLayer colorsLayer, Color colorBase)
        {
            ColorsLayer = colorsLayer;
            ColorBase = colorBase;
        }

        public void SetCharacterId(int characterId)
        {
            CharacterId = characterId;
        }
    }
}
