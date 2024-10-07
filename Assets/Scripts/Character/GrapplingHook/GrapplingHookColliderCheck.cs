using System;
using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook
{
    public class GrapplingHookColliderCheck : ACharacterMonoBehaviour
    {
        [SerializeField] private MeshCollider mesh;
        [SerializeField] private Mesh level1;

        private const float CountdownDefault = 0.1f;
        [SerializeField] private float countdown;
        [SerializeField] private bool isCountdownEnable;

        private void Start()
        {
            if (mesh) mesh.sharedMesh = null;
        }

        private void FixedUpdate()
        {
            if (isCountdownEnable == false) return;
            countdown -= Time.fixedDeltaTime;

            if (countdown <= 0)
            {
                isCountdownEnable = false;
                CharacterEntity.GrapplingHookState.SetHookDispatchState();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Const.Tags.Character))
            {
                isCountdownEnable = false;
                CharacterEntity.Character.LookAt(other.gameObject.transform.position);
                CharacterEntity.GrapplingHookState.SetHookDispatchState();
            }
        }

        public void DisableCollider()
        {
            gameObject.SetActive(false);
        }

        private void EnableCollider()
        {
            gameObject.SetActive(true);
            isCountdownEnable = true;
            countdown = CountdownDefault;
        }

        public void EnableColliderLevel1()
        {
            EnableCollider();
            if (mesh) mesh.sharedMesh = level1;
        }
    }
}
