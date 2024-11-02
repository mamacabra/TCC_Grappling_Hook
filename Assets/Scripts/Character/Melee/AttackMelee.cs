using Character.States;
using Character.Utils;
using UnityEngine;

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
                CharacterEntity.CharacterState.SetKnockbackState();
                AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.AttackParry);
                return;
            }

            if (enemy.CharacterEntity.CharacterState.State is AttackState)
            {
                enemy.CharacterEntity.CharacterState.SetKnockbackState();
                CharacterEntity.CharacterState.SetKnockbackState();
                AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.AttackParry);
            }
            else
            {
                enemy.CharacterEntity.CharacterState.SetDeathState(CharacterEntity.Character.characterBody);

                PlayersManager.Instance.AddPointsToPlayer(CharacterEntity.Character.Id,this.transform,other.transform);
                PlayersManager.Instance.PlayersToSendToCamera(other.transform, false);
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
                    CharacterEntity.CharacterState.SetParryState();
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
