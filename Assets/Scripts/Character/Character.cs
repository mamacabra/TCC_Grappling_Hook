using Character.Utils;
using Const;
using System.Collections;
using System.Collections.Generic;
using TrapSystem_Scripts.ModifierSystem;
using UnityEngine;

namespace Character
{
    public class Character : ACharacterMonoBehaviour, IModifyable
    {
        public int Id;
        public Transform characterBody;

        public bool HasShield { get; private set; } = false;
        public bool HasDashReady { get; private set; } = true;
        private const float MaxCountDownDash = 0.6f;

        public bool HasAttackReady { get; private set; } = true;

        public List<AModifier> Modifiers => modifiers;
        [SerializeField] private List<AModifier> modifiers = new();

        private const float MaxCountDownMelee = Animations.TimePerFrame * 26f; // 13 frames animation

        [Header("3D Models")]
        public GameObject crown;
        [SerializeField] private GameObject shield;

        public new void Setup(CharacterEntity entity)
        {
            base.Setup(entity);
            ToggleShield(false);
        }

        public void EnableCrown()
        {
            crown.SetActive(PlayersManager.Instance.CheckPlayerWinner(CharacterEntity.Character.Id));
        }

        public void ToggleShield(bool newStatus)
        {
            HasShield = newStatus;
            if (shield) shield.SetActive(newStatus);
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

        public bool ShouldReceiveAttack()
        {
            if (HasShield == false) return true;

            DropShield();
            return false;
        }

        public bool ShouldBeCaught()
        {
            if (HasShield == false) return true;

            DropShield();
            return false;
        }

        private void DropShield()
        {
            HasShield = false;
            CharacterEntity.CharacterPowerUp.DropPowerUp(PowerUpVariants.CharacterShieldPowerUp);
        }
    }
}
