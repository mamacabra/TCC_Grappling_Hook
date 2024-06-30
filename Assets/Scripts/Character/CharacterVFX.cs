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
            if (!spawnVfx) return;

            var main = spawnVfx.main;
            var characterColor = PlayerColorLayerManager.GetColorBase(CharacterEntity.Character.Id);
            var vfxGradienteColor = new ParticleSystem.MinMaxGradient(characterColor, characterColor);
            main.startColor = vfxGradienteColor;

            spawnVfx?.Play();
        }

        public void PlaySpawnVFXWithDelay(float delay = 0.2f)
        {
            Invoke(nameof(PlaySpawnVFX), delay);
        }

        public void StopDashVFXWithDelay(float delay = 0.2f)
        {
            Invoke(nameof(StopDashVFX), delay);
        }

        private void StopDashVFX()
        {
            dashVfx?.Stop();
        }
    }
}
