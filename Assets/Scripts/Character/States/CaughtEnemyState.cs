using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class CaughtEnemyState : ACharacterState
    {
        private readonly CharacterEntity enemy;
        private const float MinDistanceToDrop = 3.6f;
        private const float MovementSpeed = 50f;

        public CaughtEnemyState(CharacterEntity characterEntity, CharacterEntity enemy) : base(characterEntity)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            CharacterEntity.GrapplingHookState.SetHookFixEnemyState(enemy.Character.transform.position);
            enemy.CharacterState.SetHookedToEnemyState(CharacterEntity.CharacterState.transform.position);
            AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.HookHitPlayer);
        }

        public override void FixedUpdate()
        {
            var translate = Vector3.forward * (Time.fixedDeltaTime * MovementSpeed);

            var hookTransform = CharacterEntity.GrapplingHookTransform;
            hookTransform.Translate(translate * -1f);

            var enemyTransform = enemy.Character.transform;
            enemyTransform.Translate(translate);

            var distance = Vector3.Distance(CharacterEntity.Character.transform.position, hookTransform.position);
            if (distance <= MinDistanceToDrop)
            {
                CharacterEntity.CharacterState.SetWalkState();
                enemy.CharacterState.SetWalkState();
            }

            CharacterEntity.Character.MoveArrowForward();
        }

        public override void Exit()
        {
            if (enemy.CharacterState.State is HookedToEnemyState)
                enemy.CharacterState.SetWalkState();
        }
    }
}
