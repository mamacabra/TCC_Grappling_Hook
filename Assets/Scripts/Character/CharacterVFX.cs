using Character.Utils;
using UnityEngine;

namespace Character
{
    public class CharacterVFX : ACharacterMonoBehaviour
    {
        private ParticleSystem dashVfx;
        [SerializeField] private ParticleSystem glueModifierVfx;
        private ParticleSystem parryVfx;
        private ParticleSystem slashVfx;
        [SerializeField] private ParticleSystem slideModifierVfx;
        [SerializeField] private ParticleSystem spawnVfx;

        private void GetDashVFX()
        {
            dashVfx = CharacterEntity.CharacterMesh.animator?.transform.Find("vfx.Dash").GetComponent<ParticleSystem>();
        }

        public void PlayDashVFX()
        {
            if (dashVfx is null) GetDashVFX();
            dashVfx?.Play();
        }

        private void StopDashVFX()
        {
            dashVfx?.Stop();
        }

        public void StopDashVFXWithDelay(float delay = 0.2f)
        {
            Invoke(nameof(StopDashVFX), delay);
        }

        private void GetGlueModifierVFX()
        {
            glueModifierVfx = CharacterEntity.Character.transform.Find("vfx.GlueModifier").GetComponent<ParticleSystem>();
        }

        public void PlayGlueModifierVFX()
        {
            if (glueModifierVfx is null) GetGlueModifierVFX();
            glueModifierVfx?.Play();
        }

        public void StopGlueModifierVFX()
        {
            glueModifierVfx?.Stop();
        }

        private void GetSlashVFX()
        {
            slashVfx = CharacterEntity.CharacterMesh.animator?.transform.Find("vfx.Slash").GetComponent<ParticleSystem>();
        }

        public void PlaySlashVFX()
        {
            if (slashVfx is null) GetSlashVFX();
            slashVfx?.Play();
        }

        private void GetSlideModifierVFX()
        {
            slideModifierVfx = CharacterEntity.Character.transform.Find("vfx.SlideModifier").GetComponent<ParticleSystem>();
        }

        public void PlaySlideModifierVFX()
        {
            if (slideModifierVfx is null) GetSlideModifierVFX();
            slideModifierVfx?.Play();
        }

        public void StopSlideModifierVFX()
        {
            slideModifierVfx?.Stop();
        }

        private void GetSpawnVFX()
        {
            spawnVfx = CharacterEntity.Character.transform.Find("vfx.Spawn").GetComponent<ParticleSystem>();
        }

        public void PlaySpawnVFX()
        {
            if (spawnVfx is null) GetSpawnVFX();
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

        private void GetParryVFX()
        {
            parryVfx = CharacterEntity.CharacterMesh.animator?.transform.Find("vfx.Parry").GetComponent<ParticleSystem>();
        }

        public void PlayParryVFX()
        {
            if (parryVfx is null) GetParryVFX();
            parryVfx?.Play();
        }
    }
}
