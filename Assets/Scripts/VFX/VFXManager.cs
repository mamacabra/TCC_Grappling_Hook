using UnityEngine;

namespace VFX
{
    public class VFXManager : MonoBehaviour
    {
        public static VFXManager Instance;

        [SerializeField] private ParticleSystem hookDispatchVfx;
        [SerializeField] private ParticleSystem hookHitVfx;
        [SerializeField] private ParticleSystem parryVfx;
        [SerializeField] private ParticleSystem victoryVfx;

        public void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }

        public void PlayHookDispatchVFX(Vector3 position)
        {
            if (hookDispatchVfx is null) return;
            Instantiate(hookDispatchVfx, position, Quaternion.identity);
            hookDispatchVfx.Play();
        }

        public void PlayHookHitVFX(Vector3 position)
        {
            if (hookHitVfx is null) return;
            Instantiate(hookHitVfx, position, Quaternion.identity);
            hookHitVfx.Play();
        }

        public void PlayParryVFX(Vector3 position)
        {
            if (parryVfx is null) return;
            Instantiate(parryVfx, position, Quaternion.identity);
            parryVfx.Play();
        }

        public void PlayParryVFX(Vector3 position, Vector3 enemyPosition)
        {
            if (parryVfx is null) return;
            var middlePoint = (position + enemyPosition) / 2;

            Instantiate(parryVfx, middlePoint, Quaternion.identity);
            parryVfx.Play();
        }

        public void PlayVictoryVFX(Vector3 position)
        {
            if (victoryVfx is null) return;
            Instantiate(victoryVfx, position, Quaternion.identity);
            victoryVfx.Play();
        }
    }
}
