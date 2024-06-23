using System.Linq;
using Const;
using UnityEngine;

namespace Character.Utils
{
    public static class PlayerColorLayerManager
    {
        private static readonly PlayerColorLayer[] colorLayers =
        {
            new (ControlColorsLayer.Yellow, ControlColorsHex.Yellow),
            new (ControlColorsLayer.Green, ControlColorsHex.Green),
            new (ControlColorsLayer.Blue, ControlColorsHex.Blue),
            new (ControlColorsLayer.Purple, ControlColorsHex.Purple),
            new (ControlColorsLayer.Red, ControlColorsHex.Red),
            new (ControlColorsLayer.White, ControlColorsHex.White),
        };

        public static ControlColorsLayer DefineCharacterColorLayer(int characterId)
        {
            var color = colorLayers.FirstOrDefault(c => c.CharacterId == characterId);
            color ??= colorLayers.FirstOrDefault(c => c.CharacterId == PlayerColorLayer.InvalidCharacterId);

            color?.SetCharacterId(characterId);
            return color!.ColorsLayer;
        }

        public static void RemoveCharacterColorLayer(int characterId)
        {
            var color = colorLayers.FirstOrDefault(c => c.CharacterId == characterId);
            color?.SetCharacterId(PlayerColorLayer.InvalidCharacterId);
        }

        public static Color GetColorBase(int characterId)
        {
            var color = colorLayers.FirstOrDefault(c => c.CharacterId == characterId);
            return color?.ColorBase ?? Color.white;
        }
    }
}
