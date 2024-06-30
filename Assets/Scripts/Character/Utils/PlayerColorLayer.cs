using Const;
using UnityEngine;

namespace Character.Utils
{
    public class PlayerColorLayer
    {
        public const int InvalidCharacterId = -1;

        public int CharacterId { get; private set; } = InvalidCharacterId;
        public Color ColorBase { get; }
        public Color ColorBaseLight { get; }
        public ControlColorsLayer ColorsLayer { get; }

        public PlayerColorLayer(ControlColorsLayer colorsLayer, Color colorBase, Color colorBaseLight)
        {
            ColorsLayer = colorsLayer;
            ColorBase = colorBase;
            ColorBaseLight = colorBaseLight;
        }

        public void SetCharacterId(int characterId)
        {
            CharacterId = characterId;
        }
    }
}
