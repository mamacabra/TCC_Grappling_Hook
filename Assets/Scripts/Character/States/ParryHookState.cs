using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class ParryHookState : ACharacterState
    {
        private float countDown = 0.1f;

        public ParryHookState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.CharacterVFX.PlayParryVFX();
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
