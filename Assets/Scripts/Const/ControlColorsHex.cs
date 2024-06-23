using UnityEngine;

namespace Const
{
    public static class ControlColorsHex
    {
        public static Color Yellow => GetColor("#fed330");  // rgba(254, 211, 48, 1)
        public static Color Green => GetColor("#26de81");   // rgba(38, 222, 129, 1)
        public static Color Blue => GetColor("#45aaf2");    // rgba(69, 170, 242, 1)
        public static Color Purple => GetColor("#a55eea");  // rgba(165, 94, 234, 1)
        public static Color Red => GetColor("#eb3b5a");     // rgba(235, 59, 90, 1)
        public static Color White => GetColor("#dff9fb");   // rgba(223, 249, 251, 1)

        private static Color GetColor(string hex)
        {
            ColorUtility.TryParseHtmlString(hex, out var color);
            return color;
        }
    }
}
