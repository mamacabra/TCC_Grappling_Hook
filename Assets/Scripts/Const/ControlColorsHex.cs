using UnityEngine;

namespace Const
{
    public static class ControlColorsHex
    {
        public static Color YellowDark => GetColor("#B2963E");
        public static Color YellowLight => GetColor("#ffc300");
        public static Color Yellow => GetColor("#ffc300");
        public static Color GreenDark => GetColor("#398D5C");
        public static Color GreenLight => GetColor("#2ecc71");
        public static Color Green => GetColor("#2ecc71");
        public static Color BlueDark => GetColor("#5385A6");
        public static Color BlueLight => GetColor("#5dade2");
        public static Color Blue => GetColor("#5dade2");
        public static Color PurpleDark => GetColor("#623973");
        public static Color PurpleLight => GetColor("#8e44ad");
        public static Color Purple => GetColor("#8e44ad");
        public static Color RedDark => GetColor("#B35248");
        public static Color RedLight => GetColor("#e74c3c");
        public static Color Red => GetColor("#e74c3c");
        public static Color WhiteDark => GetColor("#A1A1A1");
        public static Color WhiteLight => GetColor("#f0f0f0");
        public static Color White => GetColor("#f0f0f0");

        private static Color GetColor(string hex)
        {
            ColorUtility.TryParseHtmlString(hex, out var color);
            return color;
        }
    }
}
