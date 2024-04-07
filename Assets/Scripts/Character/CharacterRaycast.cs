using Character.Utils;
using UnityEngine;

namespace Character
{
    public class CharacterRaycast : ACharacterMonoBehaviour
    {
        public bool HasHit { get; private set; }

        private const float RaycastDistance = 2f;
        private Color RaycastColor => HasHit ? Color.red : Color.green;

        private void FixedUpdate()
        {
            var t = transform;

            var direction = t.forward;
            var position = t.position;
            var origin = new Vector3(position.x, 1f, position.z);

            Physics.Raycast(origin, direction, out var hit, RaycastDistance);
            Debug.DrawRay(origin, direction * RaycastDistance, RaycastColor);

            if (hit.collider)
            {
                HasHit = hit.collider.CompareTag(Const.Tags.Wall) || hit.collider.CompareTag(Const.Tags.Object);
            }
            else
            {
                HasHit = false;
            }
        }
    }
}
