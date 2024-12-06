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
            var vfx = Instantiate(hookDispatchVfx, position, Quaternion.identity);
            ScheduleDestroy(vfx);
        }

        public void PlayHookHitVFX(Vector3 position)
        {
            if (hookHitVfx is null) return;
            var vfx = Instantiate(hookHitVfx, position, Quaternion.identity);
            ScheduleDestroy(vfx);
        }

        public void PlayParryVFX(Vector3 position)
        {
            if (parryVfx is null) return;
            var vfx = Instantiate(parryVfx, position, Quaternion.identity);
            ScheduleDestroy(vfx);
        }

        public void PlayParryVFX(Vector3 position, Vector3 enemyPosition)
        {
            if (parryVfx is null) return;
            var middlePoint = (position + enemyPosition) / 2;

            var vfx = Instantiate(parryVfx, middlePoint, Quaternion.identity);
            ScheduleDestroy(vfx);
        }

        public void PlayVictoryVFX(Vector3 position)
        {
            if (victoryVfx is null) return;
            var vfx = Instantiate(victoryVfx, position, Quaternion.identity);
            ScheduleDestroy(vfx);
        }

        private static void ScheduleDestroy(ParticleSystem vfx)
        {
            var delay = vfx.main.duration + vfx.main.startLifetime.constant;
            Destroy(vfx.gameObject, delay);
        }
    }
}
