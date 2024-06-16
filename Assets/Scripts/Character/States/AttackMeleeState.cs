using Character.Utils;
using Const;
using UnityEngine;

namespace Character.States
{
    public class AttackMeleeState : ACharacterState
    {
        private GameObject meleeHitbox;

        private float countDown;
        private float dashCountDown;
        private const float AttackDashSpeed = 35f;
        private const float TimeToEnableHitbox = Animations.TimePerFrame * 2f; // 2 frames
        private const float TimeToBeginDash = Animations.TimePerFrame * 4f; // 4 frames
        private const float TimeToStopDash = Animations.TimePerFrame * 4f; // 4 frames
        private const float TimeToDisableHitbox = TimeToEnableHitbox + Animations.TimePerFrame * 5f; // 5 frames
        private const float TimeToChangeState = TimeToDisableHitbox + (Animations.TimePerFrame * 6f); // 5 frames

        private bool attacked = false;

        public AttackMeleeState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.Character.UseAttack();
            CharacterEntity.CharacterMesh.animator?.SetTrigger("Melee");
        }

        public override void FixedUpdate()
        {
            if (countDown >= TimeToBeginDash && dashCountDown < TimeToStopDash){
                dashCountDown += Time.fixedDeltaTime;
                Walk(AttackDashSpeed, true);
            }

            countDown += Time.fixedDeltaTime;

            if (countDown >= TimeToChangeState)
                CharacterEntity.CharacterState.SetWalkState();
            else if (countDown >= TimeToDisableHitbox)
                CharacterEntity.AttackMelee.DisableHitbox();
            else if (countDown >= TimeToEnableHitbox && !attacked){
                attacked = true;
                CharacterEntity.AttackMelee.EnableHitbox();
            }
        }

        public override void Exit()
        {
            CharacterEntity.AttackMelee.DisableHitbox();
        }
    }
}
