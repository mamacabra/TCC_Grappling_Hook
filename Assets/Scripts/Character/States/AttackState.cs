using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class AttackState : ACharacterState
    {
        private GameObject meleeHitbox;
        private float countDown = 0.3f;

        public AttackState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Enter()
        {
            CharacterEntity.CharacterMesh.animator?.SetTrigger("Melee");
            meleeHitbox = Transform.Find("Body/MeleeHitbox").gameObject;
            meleeHitbox?.SetActive(true);
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
            meleeHitbox?.SetActive(false);
        }
    }
}
