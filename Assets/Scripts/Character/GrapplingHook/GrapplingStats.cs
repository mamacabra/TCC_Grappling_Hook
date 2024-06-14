using UnityEngine;

namespace Character.GrapplingHook
{
    public static class GrapplingStats
    {
        public static readonly Vector3 originPosition = new (0f, 1, 1.2f);
        public static readonly (float speed, float distance) ForceLv1 = (80, 30);
        public static readonly (float speed, float distance) ForceLv2 = (80, 40);
        public static readonly (float speed, float distance) ForceLv3 = (80, 50);
    }
}
