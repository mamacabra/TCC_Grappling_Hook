using System.Linq;
using Const;
using UnityEngine;

namespace Character.Utils
{
    public static class PlayerColorLayerManager
    {
        private static readonly PlayerColorLayer[] colorLayers =
        {
            new (ControlColors.Yellow, Color.yellow),
            new (ControlColors.Green, Color.green),
            new (ControlColors.Blue, Color.blue),
            new (ControlColors.Purple, Color.magenta),
            new (ControlColors.Red, Color.red),
            new (ControlColors.White),
        };

        public static int DefineCharacterColorLayer(int characterId)
        {
            var color = colorLayers.FirstOrDefault(c => c.CharacterId == characterId);
            color ??= colorLayers.FirstOrDefault(c => c.CharacterId == PlayerColorLayer.InvalidCharacterId);

            color?.SetCharacterId(characterId);
            return color!.ColorLayer;
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
