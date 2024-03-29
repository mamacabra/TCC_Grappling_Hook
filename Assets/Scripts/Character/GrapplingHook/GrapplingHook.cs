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
            if (other.gameObject.CompareTag(Tags.Character) == false) return;

            var character = other.gameObject.GetComponent<Character>();
            if (character == CharacterEntity.Character) return;

            var enemy = other.GetComponent<Character>();
            if (enemy.CharacterEntity.CharacterState.State is HookedState) return;

            enemy.CharacterEntity.CharacterState.SetHookedState(CharacterEntity.CharacterState.transform.position);
            CharacterEntity.CharacterState.SetRollbackHookState();
        }
    }
}
