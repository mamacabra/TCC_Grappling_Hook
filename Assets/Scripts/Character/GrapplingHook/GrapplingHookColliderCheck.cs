using System;
using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook
{
    public class GrapplingHookColliderCheck : ACharacterMonoBehaviour
    {
        [SerializeField] private MeshCollider Mesh;
        [SerializeField] private Mesh Level1;

        private void OnTriggerEnter(Collider other)
        {
            CharacterEntity.GrapplingHookState.SetHookDispatchState();
        }

        public void DisableCollider()
        {
            Mesh.gameObject.SetActive(false);
        }

        private void EnableCollider()
        {
            Mesh.gameObject.SetActive(true);
        }

        public void EnableColliderLevel1()
        {
            EnableCollider();
            Mesh.sharedMesh = Level1;
        }
    }
}
