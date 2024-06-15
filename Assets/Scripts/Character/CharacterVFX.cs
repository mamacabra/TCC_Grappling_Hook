using Character.Utils;
using UnityEngine;

namespace Character
{
    public class CharacterVFX : ACharacterMonoBehaviour
    {
        private ParticleSystem dashVfx;

        private void GetDashVFX()
        {
            dashVfx = CharacterEntity.CharacterMesh.animator?.transform.Find("vfx.Dash").GetComponent<ParticleSystem>();
        }

        public void PlayDashVFX()
        {
            if (dashVfx is null) GetDashVFX();
            dashVfx?.Play();
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
