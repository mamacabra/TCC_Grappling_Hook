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

            enemy.CharacterEntity.CharacterState.SetHookedToEnemyState(CharacterEntity.CharacterState.transform.position);
            CharacterEntity.CharacterState.SetRollbackHookState();
        }

        private static void CollideWithObject(Collider other)
        {
            Destroy(other.gameObject);
        }

        private void CollideWithWall(Collider other)
        {
            CharacterEntity.CharacterState.SetHookedToWallState(other.transform.position);
        }
    }
}
