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
            {
                Debug.Log("CARALHO");
                CharacterEntity.CharacterState.SetRollbackHookState();
            }
        }
    }
}
