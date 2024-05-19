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
        private const float MaxCountDownDash = 0.25f;

        public bool HasHookReady { get; private set; } = true;
        private const float MaxCountDownHook = 0.2f;

        public bool HasAttackReady { get; private set; } = true;
        private const float MaxCountDownMelee = 0.4f;

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

        public void UseHook()
        {
            HasHookReady = false;
        }

        private IEnumerator HookCountDownCoroutine()
        {
            HasHookReady = false;
            yield return new WaitForSeconds(MaxCountDownHook);
            HasHookReady = true;
        }

        public void StartHookCountDown()
        {
            StartCoroutine(HookCountDownCoroutine());
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
