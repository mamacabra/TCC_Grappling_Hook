using UnityEngine;

namespace Const
{
    public static class ControlColorsHex
    {
        public static Color Yellow => GetColor("#f9ca24");
        public static Color Green => GetColor("#6ab04c");
        public static Color Blue => GetColor("#4834d4");
        public static Color Purple => GetColor("#be2edd");
        public static Color Red => GetColor("#eb4d4b");
        public static Color White => GetColor("#22a6b3");

        private static Color GetColor(string hex)
        {
            ColorUtility.TryParseHtmlString(hex, out var color);
            return color;
        }
    }
}
