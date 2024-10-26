using UnityEngine;

namespace Const
{
    public static class ControlColorsHex
    {
        public static Color YellowLight => GetColor("#ffc300");
        public static Color Yellow => GetColor("#ffc300");
        public static Color GreenLight => GetColor("#2ecc71");
        public static Color Green => GetColor("#2ecc71");
        public static Color BlueLight => GetColor("#5dade2");
        public static Color Blue => GetColor("#5dade2");
        public static Color PurpleLight => GetColor("#8e44ad");
        public static Color Purple => GetColor("#8e44ad");
        public static Color RedLight => GetColor("#e74c3c");
        public static Color Red => GetColor("#e74c3c");
        public static Color WhiteLight => GetColor("#f0f0f0");
        public static Color White => GetColor("#f0f0f0");

        private static Color GetColor(string hex)
        {
            ColorUtility.TryParseHtmlString(hex, out var color);
            return color;
        }
    }
}
