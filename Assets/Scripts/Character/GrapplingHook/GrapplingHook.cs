using Character.States;
using Character.Utils;
using Const;
using UnityEngine;

namespace Character.GrapplingHook
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class GrapplingHook : ACharacterMonoBehaviour
    {
        public int Force { get; private set; }
        private const int MaxGrapplingHookForce = 3;
        private const int DefaultGrapplingHookForce = 1;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Character))
                CollideWithCharacter(other);
            else if (other.gameObject.CompareTag(Tags.Wall))
                CollideWithWall(other);
            else if (other.gameObject.CompareTag(Tags.Object))
                CollideWithObject(other);
        }

        private void CollideWithCharacter(Collider other)
        {
            var character = other.gameObject.GetComponent<Character>();
            if (character == CharacterEntity.Character) return;

            var enemy = other.GetComponent<Character>();
            if (enemy.CharacterEntity.CharacterState.State is HookedToEnemyState) return;

            if (enemy.ShouldBeCaught() == false)
            {
                CharacterEntity.GrapplingHookState.SetHookRollbackState();
                return;
            }

            CharacterEntity.CharacterState.SetCaughtEnemyState(enemy.CharacterEntity);
        }

        private static void CollideWithObject(Collider other)
        {
            Destroy(other.gameObject);
        }

        private void CollideWithWall(Collider other)
        {
            var hasHit = false;
            var origin = new Vector3(transform.position.x, 1f, transform.position.z);
            var direction = (other.transform.position - transform.position).normalized;

            var hits = Physics.RaycastAll(origin, direction, 100f);
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject != other.transform.gameObject) continue;

                hasHit = true;
                CharacterEntity.CharacterState.SetHookedToWallState(hit.point);
                break;
            }

            if (hasHit == false)
                CharacterEntity.CharacterState.SetHookedToWallState(other.transform.position);
        }

        public void IncreaseHookForce()
        {
            Force += 1;
            if (Force > MaxGrapplingHookForce) Force = MaxGrapplingHookForce;
        }

        public void ResetHookForce()
        {
            Force = DefaultGrapplingHookForce;
        }
    }
}
