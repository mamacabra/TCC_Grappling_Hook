using UnityEngine;

namespace VFX
{
    public class VFXManager : MonoBehaviour
    {
        public static VFXManager Instance;

        [SerializeField] private ParticleSystem parryVfx;

        public void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }

        public void PlayParryVFX(Vector3 position)
        {
            Instantiate(parryVfx, position, Quaternion.identity);
            parryVfx.Play();
        }

        public void PlayParryVFX(Vector3 position, Vector3 enemyPosition)
        {
            var middlePoint = (position + enemyPosition) / 2;

            Instantiate(parryVfx, middlePoint, Quaternion.identity);
            parryVfx.Play();
        }
    }
}
