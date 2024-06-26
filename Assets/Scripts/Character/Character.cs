using System;
using Character.Utils;
using Const;
using System.Collections;
using UnityEngine;

namespace Character
{
    public class Character : ACharacterMonoBehaviour
    {
        public int Id;
        public Transform characterBody;
        public GameObject crown;
        public new CharacterEntity CharacterEntity { get; private set; }

        public bool HasDashReady { get; private set; } = true;
        private const float MaxCountDownDash = 0.6f;

        public bool HasAttackReady { get; private set; } = true;
        private const float MaxCountDownMelee = Animations.TimePerFrame * 26f; // 13 frames animation

        public new void Setup(CharacterEntity entity)
        {
            CharacterEntity = entity;
        }

        public void EnableCrown()
        {
            //crown.SetActive(PlayersManager.Instance.CheckPlayerWinner(CharacterEntity.Character.Id));
        }

        public void UseDash()
        {
            StartCoroutine(DashCountDownCoroutine());
        }

        private IEnumerator DashCountDownCoroutine()
        {
            HasDashReady = false;
            yield return new WaitForSeconds(MaxCountDownDash);
            HasDashReady = true;
        }

        public void UseAttack()
        {
            StartCoroutine(AttackCountDownCoroutine());
        }

        private IEnumerator AttackCountDownCoroutine()
        {
            HasAttackReady = false;
            yield return new WaitForSeconds(MaxCountDownMelee);
            HasAttackReady = true;
        }

        public void LookAt(Vector3 direction)
        {
            if (direction == Vector3.zero) return;

            direction.y = 0;
            CharacterEntity.Character.characterBody.LookAt(direction);
        }
    }
}
