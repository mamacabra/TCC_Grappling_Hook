using UnityEngine;

namespace Character.GrapplingHook
{
    public static class GrapplingStats
    {
        public static readonly Vector3 originPosition = new (0f, 1, 1.2f);
        public static readonly (float speed, float distance) ForceLv1 = (90, 30);
        public static readonly (float speed, float distance) ForceLv2 = (100, 40);
        public static readonly (float speed, float distance) ForceLv3 = (100, 50);
        public static readonly float RollbackSpeed = 100;
    }
}
