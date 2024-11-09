using Character.Utils;
using UnityEngine;
using VFX;

namespace Character.States
{
    public class ParryHookState : ACharacterState
    {
        private float countDown = 0.1f;

        public ParryHookState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            var characterTransform = CharacterEntity.Character.transform;
            var vfxPosition = characterTransform.position + characterTransform.forward * 2;
            VFXManager.Instance.PlayParryVFX(vfxPosition);
            AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.AttackParry);
        }

        public override void FixedUpdate()
        {
            countDown -= Time.fixedDeltaTime;
            if (countDown < 0)
                CharacterEntity.CharacterState.SetWalkState();
        }
    }
}
