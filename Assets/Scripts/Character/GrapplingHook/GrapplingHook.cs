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
                CollideWithWall();
            else if (other.gameObject.CompareTag(Tags.Object))
                CollideWithObject(other);
        }

        private void CollideWithCharacter(Collider other)
        {
            var character = other.gameObject.GetComponent<Character>();
            if (character == CharacterEntity.Character) return;

            var enemy = other.GetComponent<Character>();
            if (enemy.CharacterEntity.CharacterState.State is HookedState) return;

            enemy.CharacterEntity.CharacterState.SetHookedState(CharacterEntity.CharacterState.transform.position);
            CharacterEntity.CharacterState.SetRollbackHookState();
        }

        private static void CollideWithObject(Collider other)
        {
            Debug.Log("Collide with object");
            Destroy(other.gameObject);
        }

        private void CollideWithWall()
        {
            CharacterEntity.CharacterState.SetRollbackHookState();
        }
    }
}
