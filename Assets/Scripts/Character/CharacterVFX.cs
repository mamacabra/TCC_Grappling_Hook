using Character.Utils;
using UnityEngine;

namespace Character
{
    public class CharacterVFX : ACharacterMonoBehaviour
    {
        [SerializeField] private ParticleSystem dashVfx;
        [SerializeField] private ParticleSystem parryVfx;
        [SerializeField] private ParticleSystem spawnVfx;

        private void GetDashVFX()
        {
            dashVfx = CharacterEntity.CharacterMesh.animator?.transform.Find("vfx.Dash").GetComponent<ParticleSystem>();
        }

        private void GetParryVFX()
        {
            parryVfx = CharacterEntity.CharacterMesh.animator?.transform.Find("vfx.Parry").GetComponent <ParticleSystem>();
        }

        private void GetSpawnVFX()
        {
            spawnVfx = CharacterEntity.CharacterMesh.animator?.transform.Find("vfx.Spawn").GetComponent<ParticleSystem>();
        }

        public void PlayDashVFX()
        {
            if (dashVfx is null) GetDashVFX();
            dashVfx?.Play();
        }

        public void PlayParryVFX()
        {
            if(parryVfx is null) GetParryVFX();
            parryVfx?.Play();
        }

        public void PlaySpawnVFX()
        {
            if(spawnVfx is null) GetSpawnVFX();
            spawnVfx?.Play();
        }

        public void StopDashVFXWithDelay(float delay = 0.2f)
        {
            Invoke(nameof(StopDashVFX), delay);
        }

        public void StopParryVFXWithDelay(float delay = 0.2f)
        {
            Invoke(nameof(StopParryVFX), delay);
        }
        private void StopDashVFX()
        {
            dashVfx?.Stop();
        }

        private void StopParryVFX()
        {
            parryVfx.Stop();
        }

        private void StopSpawnVFX()
        {
            spawnVfx.Stop();
        }
    }
}
