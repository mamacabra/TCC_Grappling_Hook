using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class AttackState : ACharacterState
    {
        private GameObject meleeHitbox;
        private float countDown = 0.4f;

        public AttackState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Enter()
        {
            CharacterEntity.Character.UseAttack();
            CharacterEntity.AttackMelee.EnableHitbox();
            CharacterEntity.CharacterMesh.animator?.SetTrigger("Melee");
        }

        public override void Update()
        {
            countDown -= Time.deltaTime;

            if (countDown < 0f)
            {
                CharacterEntity.CharacterState.SetWalkState();
            }
        }

        public override void Exit()
        {
            CharacterEntity.AttackMelee.DisableHitbox();
        }
    }
}
