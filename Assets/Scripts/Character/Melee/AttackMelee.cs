using Character.States;
using Character.Utils;
using UnityEngine;

namespace Character.Melee
{
    public class AttackMelee : ACharacterMonoBehaviour
    {
        [SerializeField] private GameObject hitbox;

        private void OnTriggerEnter(Collider other)
        {
            if (CharacterEntity.CharacterState.State is DeathState) return;
            if (other.CompareTag("Character") == false) return;

            var enemy = other.GetComponent<Character>();
            if (enemy == null) return;
            if (enemy.CharacterEntity.CharacterState.State is DeathState) return;

            if (enemy.CharacterEntity.CharacterState.State is AttackState)
            {
                enemy.CharacterEntity.CharacterState.SetKnockbackState();
                CharacterEntity.CharacterState.SetKnockbackState();
            }
            else
            {
                enemy.CharacterEntity.CharacterState.SetDeathState();
                PlayersManager.Instance.AddPointsToPlayer(CharacterEntity.Character.Id);
            }
        }

        public void DisableHitbox()
        {
            hitbox?.SetActive(false);
        }

        public void EnableHitbox()
        {
            hitbox?.SetActive(true);
        }
    }
}
