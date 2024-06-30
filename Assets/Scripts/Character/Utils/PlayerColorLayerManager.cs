using System.Linq;
using Const;
using UnityEngine;

namespace Character.Utils
{
    public static class PlayerColorLayerManager
    {
        private static readonly PlayerColorLayer[] colorLayers =
        {
            new (ControlColorsLayer.Yellow, ControlColorsHex.Yellow, ControlColorsHex.YellowLight),
            new (ControlColorsLayer.Green, ControlColorsHex.Green, ControlColorsHex.GreenLight),
            new (ControlColorsLayer.Blue, ControlColorsHex.Blue, ControlColorsHex.BlueLight),
            new (ControlColorsLayer.Purple, ControlColorsHex.Purple, ControlColorsHex.PurpleLight),
            new (ControlColorsLayer.Red, ControlColorsHex.Red, ControlColorsHex.RedLight),
            new (ControlColorsLayer.White, ControlColorsHex.White, ControlColorsHex.WhiteLight),
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

        public static Color GetColorBaseLight(int characterId)
        {
            var color = colorLayers.FirstOrDefault(c => c.CharacterId == characterId);
            return color?.ColorBaseLight ?? Color.white;
        }
    }
}
