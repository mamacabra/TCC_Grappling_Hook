using System.Linq;

namespace Character.Utils
{
    public static class PlayerColorLayerManager
    {
        public static string bolinhas = "PlayerColorLayerManager";

        private static readonly PlayerColorLayer[] colors =
        {
            new (0, true),
            new (1, true),
            new (2, true),
            new (3, true),
            new (4, true),
            new (5, true),
        };

        public static int GetAvailableColorLayer()
        {
            var color = colors.FirstOrDefault(color => color.IsAvailable);
            color?.SetAvailable(false);
            return color!.Layer;
        }

        public static void DisableColorLayer(int layer)
        {
            colors[layer].SetAvailable(false);
        }
    }
}
