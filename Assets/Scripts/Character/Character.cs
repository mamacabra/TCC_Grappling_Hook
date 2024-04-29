using Character.Utils;
using System.Collections;
using UnityEngine;

namespace Character
{
    public class Character : ACharacterMonoBehaviour
    {
        public int Id;
        public new CharacterEntity CharacterEntity { get; }

        public bool HasDashReady { get; private set; } = true;
        private const float MaxCountDownDash = 0.2f;

        public bool HasHookReady { get; private set; } = true;
        private const float MaxCountDownHook = 0.2f;

        public bool HasAttackMeleeReady { get; private set; } = true;
        private const float MaxCountDownMelee = 0.5f;

        public void UseDash()
        {
            HasDashReady = false;
        }

        private IEnumerator DashCountDownCoroutine()
        {
            HasDashReady = false;
            yield return new WaitForSeconds(MaxCountDownDash);
            HasDashReady = true;
        }

        public void StartDashCountDown()
        {
            StartCoroutine(DashCountDownCoroutine());
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

        private IEnumerator AttackMeleeCountDownCoroutine()
        {
            HasAttackMeleeReady = false;
            yield return new WaitForSeconds(MaxCountDownMelee);
            HasAttackMeleeReady = true;
        }

        public void StartAttackMeleeCountDown()
        {
            StartCoroutine(AttackMeleeCountDownCoroutine());
        }
    }
}
