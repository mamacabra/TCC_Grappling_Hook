using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class AttackMeleeState : ACharacterState
    {
        private GameObject meleeHitbox;

        private float countDown;
        private const float TimePerFrame = 0.03f; // 30 FPS
        private const float TimeToEnableHitbox = TimePerFrame * 2f; // 2 frames
        private const float TimeToDisableHitbox = TimeToEnableHitbox + TimePerFrame * 5; // 5 frames

        

        public AttackMeleeState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Enter()
        {
            CharacterEntity.Character.UseAttack();
            CharacterEntity.CharacterMesh.animator?.SetTrigger("Melee");
            
            AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.AttackMiss);
        }

        public override void Update()
        {
            Walk();
            LookAt();
            
        }

        public override void FixedUpdate()
        {
            countDown += Time.fixedDeltaTime;

            if (countDown >= TimeToEnableHitbox && CharacterEntity.AttackMelee.IsHitboxEnabled == false)
                CharacterEntity.AttackMelee.EnableHitbox();

            if (countDown >= TimeToDisableHitbox && CharacterEntity.AttackMelee.IsHitboxEnabled)
                CharacterEntity.CharacterState.SetWalkState();

            
        }
        


        public override void Exit()
        {
            CharacterEntity.AttackMelee.DisableHitbox();
        }
    }
}
