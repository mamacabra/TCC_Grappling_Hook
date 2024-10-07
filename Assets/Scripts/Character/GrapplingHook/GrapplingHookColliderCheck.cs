using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook
{
    public class GrapplingHookColliderCheck : ACharacterMonoBehaviour
    {
        [SerializeField] private MeshCollider mesh;
        [SerializeField] private Mesh level1;
        [SerializeField] private Mesh level2;
        [SerializeField] private Mesh level3;

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
                CharacterEntity.Character.LookAt(other.gameObject.transform.position);
                CharacterEntity.GrapplingHookState.SetHookDispatchState();
            }
        }

        public void DisableCollider()
        {
            gameObject.SetActive(false);
            isCountdownEnable = false;
        }

        public void EnableCollider()
        {
            gameObject.SetActive(true);
            isCountdownEnable = true;
            countdown = CountdownDefault;

            if (mesh)
            {
                mesh.sharedMesh = CharacterEntity.Hook.Force switch
                {
                    1 => level1,
                    2 => level2,
                    _ => level3
                };
            }
        }
    }
}
