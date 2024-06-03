using Character.Utils;
using System.Collections;
using UnityEngine;

namespace Character
{
    public class Character : ACharacterMonoBehaviour
    {
        public int Id;
        public Transform characterBody;
        public new CharacterEntity CharacterEntity { get; private set; }

        public bool HasDashReady { get; private set; } = true;
        private const float MaxCountDownDash = 0.3f;

        public bool HasAttackReady { get; private set; } = true;
        private const float MaxCountDownMelee = 0.3f;

        public new void Setup(CharacterEntity entity)
        {
            CharacterEntity = entity;
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
    }
}
