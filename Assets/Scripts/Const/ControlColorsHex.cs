using UnityEngine;

namespace Const
{
    public static class ControlColorsHex
    {
        public static Color YellowLight => GetColor("#fee892");
        public static Color Yellow => GetColor("#fed330");
        public static Color GreenLight => GetColor("#7bebb2");
        public static Color Green => GetColor("#26de81");
        public static Color BlueLight => GetColor("#a1d4f8");
        public static Color Blue => GetColor("#45aaf2");
        public static Color PurpleLight => GetColor("#d5b5f5");
        public static Color Purple => GetColor("#a55eea");
        public static Color RedLight => GetColor("#f494a5");
        public static Color Red => GetColor("#eb3b5a");
        public static Color WhiteLight => GetColor("#f0fcfd");
        public static Color White => GetColor("#dff9fb");

        private static Color GetColor(string hex)
        {
            ColorUtility.TryParseHtmlString(hex, out var color);
            return color;
        }
    }
}
