using Character.Utils;
using Const;
using UnityEngine;

namespace Character.States
{
    public class AttackMeleeState : ACharacterState
    {
        private GameObject meleeHitbox;

        private float countDown;
        private const float AttackDashSpeed = 25f;
        private const float TimeToEnableHitbox = Animations.TimePerFrame * 2f; // 2 frames
        private const float TimeToDisableHitbox = TimeToEnableHitbox + Animations.TimePerFrame * 5f; // 5 frames
        private const float TimeToChangeState = TimeToDisableHitbox + Animations.TimePerFrame * 5f; // 5 frames

        private bool attacked = false;

        public AttackMeleeState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.Character.UseAttack();
            CharacterEntity.CharacterMesh.animator?.SetTrigger("Melee");
        }

        public override void Update()
        {
            Walk(AttackDashSpeed, true);
            //LookAt();
        }

        public override void FixedUpdate()
        {
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
