using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class DashState : ACharacterState
    {
        private float countDown;
        private const float DashSpeed = 70f;
        private const float DashDuration = 0.2f;
        private const float DashDecelerationTimePercent = 0.5f;
        private const float DashDisableAnimationTimePercent = 0.8f;

        public DashState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.Character.UseDash();
            CharacterEntity.CharacterMesh.animator?.SetFloat("Speed", 0);
            CharacterEntity.CharacterMesh.animator?.SetBool("isDash", true);
            CharacterEntity.CharacterVFX.PlayDashVFX();
            CharacterEntity.GrapplingHookState.SetHookReadyState();
        }

        public override void FixedUpdate()
        {
            countDown += Time.fixedDeltaTime;

            if (countDown > DashDuration * DashDecelerationTimePercent)
            {
                var timePercent = Mathf.Floor((countDown * 100) / DashDuration);
                var outSpeed = DashSpeed - DashSpeed * (timePercent / 100);

                Walk(outSpeed, true);
            }
            else
            {
                Walk(DashSpeed, true);
            }

            if (countDown > DashDuration * DashDecelerationTimePercent)
            {
                CharacterEntity.CharacterMesh.animator?.SetBool("isDash", false);
                CharacterEntity.CharacterVFX.StopDashVFXWithDelay();
            }

            if (countDown > DashDuration)
            {
                CharacterEntity.CharacterState.SetPainState();
            }
        }

        public override void Exit()
        {
            CharacterEntity.CharacterMesh.animator?.SetBool("isDash", false);
            CharacterEntity.CharacterVFX.StopDashVFXWithDelay();
        }
    }
}
