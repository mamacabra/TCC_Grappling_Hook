using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class AttackState : ACharacterState
    {
        public AttackState(CharacterEntity characterEntity) : base(characterEntity) { }
        public GameObject meleeHitbox;
        private float timer = 0.3f;
        private bool isMeleeing;

        public override void Enter()
        {
            meleeHitbox = Transform.Find("MeleeHitbox").gameObject;
            ActivateHitbox();
        }

        public override void Update()
        {
            if (isMeleeing)
            {
                timer -= Time.deltaTime;

            }

            if (timer < 0f)
            {
                DeactivateHitbox();
            }
        }

        private void ActivateHitbox()
        {
            meleeHitbox?.SetActive(true);
            isMeleeing = true;
            
        }

        private void DeactivateHitbox()
        {
            meleeHitbox?.SetActive(false);
            isMeleeing = false;
            timer = 0.3f;
            CharacterEntity.CharacterState.SetWalkState();
        }

        // public override void Exit(){
        //     DeactivateHitbox();
        // }
    }
}

