using Character.States;
using Character.Utils;
using UnityEngine;
using VFX;
using LocalMultiplayer;

namespace Character.Melee
{
    public class AttackMelee : ACharacterMonoBehaviour
    {
        [SerializeField] private GameObject hitbox;

        private const float SphereCastRadius = 2.0f;
        private const float SphereCastDistance = 2.0f;

        private void Update()
        {
            Parrynator();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (CharacterEntity.CharacterState.State is DeathState) return;
            if (other.CompareTag("Character") == false) return;

            var enemy = other.GetComponent<Character>();
            if (enemy == null) return;
            if (enemy.CharacterEntity.CharacterState.State is DeathState) return;

            if (enemy.ShouldReceiveAttack() == false)
            {
                CharacterEntity.CharacterState.SetParryAttackState();
                AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.AttackParry);
                return;
            }

            var characterForward = CharacterEntity.Character.transform.forward;
            var enemyForward = enemy.CharacterEntity.Character.transform.forward;

            if (Vector3.Dot(characterForward, enemyForward) < 0 && enemy.CharacterEntity.CharacterState.State is AttackState)
            {
                VFXManager.Instance.PlayParryVFX(CharacterEntity.Character.transform.position, enemy.CharacterEntity.Character.transform.position);
                enemy.CharacterEntity.CharacterState.SetParryAttackState();
                CharacterEntity.CharacterState.SetParryAttackState();
                AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.AttackParry);
                CinemachineShake.Instance.ShakeCamera(1.0f, 0.4f);
                CharacterEntity.GamepadVibrate.RumblePulse(0.1f, 0.2f, 0.4f);
            }
            else
            {
                enemy.CharacterEntity.CharacterState.SetDeathState(CharacterEntity.Character.characterBody);
                PlayersManager.Instance.AddPointsToPlayer(CharacterEntity.Character.Id,this.transform,other.transform);
                PlayersManager.Instance.PlayersToSendToCamera(other.transform, false);
                CinemachineShake.Instance.ShakeCamera(0.5f, 0.2f);
                CharacterEntity.GamepadVibrate.RumblePulse(0.1f, 0.2f, 0.4f);
            }
        }

        private void Parrynator()
        {
            Vector3 origin = CharacterEntity.Character.transform.position;
            RaycastHit hit;

            if (Physics.SphereCast(origin, SphereCastRadius, CharacterEntity.Character.transform.forward, out hit, SphereCastDistance))
            {
                if (hit.collider.CompareTag("GrapplingHook"))
                {
                    CharacterEntity.CharacterState.SetParryHookState();
                    var hook = hit.collider.gameObject.GetComponent<GrapplingHook.GrapplingHook>();
                    hook.CharacterEntity.CharacterState.SetRollbackHookState();
                }
            }
        }

        public void DisableHitbox()
        {
            hitbox?.SetActive(false);
        }

        public void EnableHitbox()
        {
            hitbox?.SetActive(true);
            AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.AttackMiss);
        }
    }
}
