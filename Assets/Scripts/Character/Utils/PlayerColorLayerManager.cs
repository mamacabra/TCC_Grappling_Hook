using System.Linq;

namespace Character.Utils
{
    public static class PlayerColorLayerManager
    {
        private static readonly PlayerColorLayer[] colorLayers =
        {
            new (0),
            new (1),
            new (2),
            new (3),
            new (4),
            new (5),
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
    }
}
