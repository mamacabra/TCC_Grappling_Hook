using System.Linq;
using Const;
using UnityEngine;

namespace Character.Utils
{
    public static class PlayerColorLayerManager
    {
        private static readonly PlayerColorLayer[] colorLayers =
        {
            new (ControlColorsLayer.Yellow, ControlColorsHex.Yellow, ControlColorsHex.YellowLight, ControlColorsHex.YellowDark),
            new (ControlColorsLayer.Green, ControlColorsHex.Green, ControlColorsHex.GreenLight, ControlColorsHex.GreenDark),
            new (ControlColorsLayer.Blue, ControlColorsHex.Blue, ControlColorsHex.BlueLight, ControlColorsHex.BlueDark),
            new (ControlColorsLayer.Purple, ControlColorsHex.Purple, ControlColorsHex.PurpleLight, ControlColorsHex.PurpleDark),
            new (ControlColorsLayer.Red, ControlColorsHex.Red, ControlColorsHex.RedLight, ControlColorsHex.RedDark),
            new (ControlColorsLayer.White, ControlColorsHex.White, ControlColorsHex.WhiteLight, ControlColorsHex.WhiteDark),
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

        public static Color GetColorBaseDark(int characterId)
        {
            var color = colorLayers.FirstOrDefault(c => c.CharacterId == characterId);
            return color?.ColorBaseDark ?? Color.white;
        }
    }
}
