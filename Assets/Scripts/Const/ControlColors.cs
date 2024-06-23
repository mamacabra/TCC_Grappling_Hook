using UnityEngine;

namespace Const
{
    public enum ControlColorsLayer
    {
        Yellow = 6,
        Green = 8,
        Blue = 9,
        Purple = 10,
        Red = 11,
        White = 1,
    }

    public static class ControlColors
    {
        public const int Yellow = 6;
        public const int Green = 8;
        public const int Blue = 9;
        public const int Purple = 10;
        public const int Red = 11;
        public const int White = 12;
    }

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
