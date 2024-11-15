using UnityEngine;

namespace Character.GrapplingHook
{
    public static class GrapplingStats
    {
        public static readonly Vector3 OriginPosition = new (0f, 1f, 1.2f);
        public static readonly (float speed, float distance) ForceLv1 = (100f, 35f);
        public static readonly (float speed, float distance) ForceLv2 = (110f, 50f);
        public static readonly (float speed, float distance) ForceLv3 = (120f, 65f);
        public const float RollbackSpeed = 150f;

        public static readonly Vector3 HookColliderDispatchSize = new (3f, 1f, 3f);
        public static readonly Vector3 HookColliderRollbackSize = new (4f, 1f, 4f);
    }
}
