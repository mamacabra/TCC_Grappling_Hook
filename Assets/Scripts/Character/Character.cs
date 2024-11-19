using Character.States;
using Character.Utils;
using Const;
using System.Collections;
using System.Collections.Generic;
using TrapSystem_Scripts.ModifierSystem;
using UnityEngine;
using LocalMultiplayer;

namespace Character
{
    public class Character : ACharacterMonoBehaviour, IModifyable
    {
        public int Id;
        public Transform characterBody;

        public bool HasShield { get; private set; } = false;
        public bool HasSpeedBoost { get; private set; } = false;
        private const float MaxCountDownSpeedBoost = 6f; // 6 seconds

        public bool HasDashReady { get; private set; } = true;
        private const float MaxCountDownDash = 0.8f;

        public bool HasAttackReady { get; private set; } = true;
        private const float MaxCountDownAttack = 0.8f;

        public Vector3 CurrentSpeed;
        public List<AModifier> Modifiers => modifiers;
        [SerializeField] private List<AModifier> modifiers = new();


        [Header("3D Models")]
        public GameObject crown;
        [SerializeField] private GameObject shield;

        public new void Setup(CharacterEntity entity)
        {
            base.Setup(entity);
        }

        public void EnableCrown()
        {
            crown.SetActive(PlayersManager.Instance.CheckPlayerWinner(CharacterEntity.Character.Id));
        }

        public void ToggleShield(bool newStatus)
        {
            HasShield = newStatus;
            if (shield) shield.SetActive(newStatus);
            else
            {
                transform.Find("Shield").gameObject.SetActive(newStatus);
            }
        }

        public void ToggleSpeedBoost(bool newStatus)
        {
            HasSpeedBoost = newStatus;
            if (newStatus) StartCoroutine(SpeedBoostCountDownCoroutine());
        }

        private IEnumerator SpeedBoostCountDownCoroutine()
        {
            yield return new WaitForSeconds(MaxCountDownSpeedBoost);
            CharacterEntity.CharacterPowerUp.DropPowerUp(PowerUpVariants.CharacterSpeedBoostPowerUp);
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
            yield return new WaitForSeconds(MaxCountDownAttack);
            HasAttackReady = true;
        }

        public void LookAt(Vector3 direction)
        {
            if (direction == Vector3.zero) return;

            direction.y = 0.5f;
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
            AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.PowerUpShieldBreak);
        }

        public void PlaySpawnAnims()
        {
            if (CharacterEntity.CharacterState.State is not ReadyState) return;

            AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.PlayerSpawn);
            CharacterEntity.CharacterMesh.GetMeshParent.SetActive(true);
            CharacterEntity.GrapplingHookTransform.gameObject.SetActive(true);
            CharacterEntity.GrapplingHookRope.SetActive(true);
            CharacterEntity.CharacterMesh.animator?.SetTrigger("onSpawn");
        }

        public void MoveArrowForward(float To = 0.0f, float speed = 10.0f)
        {
            var dirArrowTransform = CharacterEntity.CharacterMesh.characterItemsHandle.directionArrow.transform;
            dirArrowTransform.localPosition = new Vector3(0.0f, 0.0f, Mathf.MoveTowards(dirArrowTransform.localPosition.z, To, Time.deltaTime * speed));
        }
    }
}
