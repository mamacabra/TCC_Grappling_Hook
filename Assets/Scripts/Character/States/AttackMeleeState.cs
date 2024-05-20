using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class AttackMeleeState : ACharacterState
    {
        private GameObject meleeHitbox;

        private float countDown;
        private const float TimeToEnableHitbox = 0.1f;
        private const float TimeToDisableHitbox = 0.3f;
        private float WalkSpeed = 10f;

        public AttackMeleeState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Enter()
        {
            CharacterEntity.Character.UseAttack();
            CharacterEntity.CharacterMesh.animator?.SetTrigger("Melee");
        }

        public override void Update()
        {
            Walk();
            LookAt();
        }

        public override void FixedUpdate()
        {
            countDown += Time.fixedDeltaTime;

            if (countDown >= TimeToEnableHitbox)
                CharacterEntity.AttackMelee.EnableHitbox();

            if (countDown >= TimeToDisableHitbox)
                CharacterEntity.CharacterState.SetWalkState();
        }

        public override void Exit()
        {
            CharacterEntity.AttackMelee.DisableHitbox();
        }
    }
}
