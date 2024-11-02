using Character.Utils;
using Const;
using UnityEngine;

namespace Character.States
{
    public class AttackState : ACharacterState
    {
        private GameObject meleeHitbox;

        private float countDown;
        private float dashCountDown;

        private const float JoystickDeadZone = 0.2f;
        private const float AttackWalkSpeed = 20f;
        private const float AttackWalkSpeedExtra = 26f;
        private const float AttackRotationSpeed = 3f;
        private const float AttackDecelerationTimePercent = 0.6f;
        private const float AttackDuration = TimeToDisableHitbox + (Animations.TimePerFrame * 6f); // 5 frames

        private const float TimeToEnableHitbox = Animations.TimePerFrame * 2f; // 2 frames
        private const float TimeToDisableHitbox = TimeToEnableHitbox + Animations.TimePerFrame * 5f; // 5 frames

        private bool attacked;

        public AttackState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.Character.UseAttack();
            CharacterEntity.CharacterVFX.PlaySlashVFX();
            CharacterEntity.CharacterMesh.animator?.SetBool("isDash", false);
            CharacterEntity.CharacterMesh.animator?.SetBool("isAttacking", true);
            CharacterEntity.CharacterMesh.animator?.SetTrigger("Melee");
            CharacterEntity.GrapplingHookState.SetHookReadyState();
        }

        public override void FixedUpdate()
        {
            countDown += Time.fixedDeltaTime;

            var directionMagnitude = CharacterEntity.CharacterInput.MoveDirection.magnitude;
            if (directionMagnitude > JoystickDeadZone)
            {
                var outSpeed = AttackWalkSpeed;
                switch (countDown)
                {
                    case < AttackDuration * AttackDecelerationTimePercent:
                    {
                        var timePercent = Mathf.Floor((countDown * 100) / AttackDuration) / 100;
                        outSpeed = AttackWalkSpeed + (AttackWalkSpeedExtra * timePercent) * 2;
                        break;
                    }
                    case > AttackDuration * AttackDecelerationTimePercent:
                    {
                        var timePercent = Mathf.Floor((countDown * 100) / AttackDuration) / 100;
                        outSpeed = AttackWalkSpeed * timePercent;
                        break;
                    }
                }


                Walk(outSpeed, true);
            }

            switch (countDown)
            {
                case >= AttackDuration:
                    CharacterEntity.CharacterState.SetWalkState();
                    break;
                case >= TimeToDisableHitbox:
                    CharacterEntity.AttackMelee.DisableHitbox();
                    break;
                case >= TimeToEnableHitbox when attacked == false:
                    attacked = true;
                    CharacterEntity.AttackMelee.EnableHitbox();
                    break;
            }

            LookAt(AttackRotationSpeed);
        }

        public override void Exit()
        {
            CharacterEntity.AttackMelee.DisableHitbox();
            CharacterEntity.CharacterMesh.animator?.SetBool("isAttacking", false);

            if(PlayersManager.Instance.GameOver)
                CameraManager.Instance.DeathFeedBack();
        }
    }
}
