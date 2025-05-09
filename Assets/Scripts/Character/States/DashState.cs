using Character.Utils;
using TrapSystem_Scripts.ModifierSystem;
using UnityEngine;

namespace Character.States
{
    public class DashState : ACharacterState
    {
        private float countDown;
        private float dashDuration = 0.3f;
        private const float DashSpeed = 70f;
        private const float DashDecelerationTimePercent = 0.4f;

        public DashState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.Character.UseDash();
            AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.PlayerDash);
            CharacterEntity.CharacterMesh.animator?.SetFloat("Speed", 0);
            CharacterEntity.CharacterMesh.animator?.SetBool("isDash", true);
            CharacterEntity.CharacterVFX.PlayDashVFX();
            CharacterEntity.GrapplingHookState.SetHookReadyState();
        }

        public override void FixedUpdate()
        {
            countDown += Time.fixedDeltaTime;

            var hasGlueModifier = false;
            foreach (var modifier in CharacterEntity.Character.Modifiers) {
                hasGlueModifier = modifier is GlueModifier;
            }

            if (hasGlueModifier) dashDuration *= 0.6f;

            if (countDown > dashDuration * DashDecelerationTimePercent && CharacterEntity.Character.Modifiers.Count == 0)
            {
                var timePercent = Mathf.Floor((countDown * 100) / dashDuration);
                var outSpeed = DashSpeed - DashSpeed * (timePercent / 100);

                Walk(outSpeed, true);
            }
            else
            {
                Walk(DashSpeed, true);
            }

            if (countDown > dashDuration * DashDecelerationTimePercent)
            {
                CharacterEntity.CharacterMesh.animator?.SetBool("isDash", false);
                CharacterEntity.CharacterVFX.StopDashVFXWithDelay();
            }

            if (countDown > dashDuration)
            {
                CharacterEntity.CharacterState.SetWalkState();
            }
        }

        public override void Exit()
        {
            base.Exit();
            CharacterEntity.CharacterMesh.animator?.SetBool("isDash", false);
            CharacterEntity.CharacterVFX.StopDashVFXWithDelay();
        }
    }
}
