using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class ParryState : ACharacterState
    {
        private float parryDuration = 1.0f;
        private float parryTimer;
        private bool isParrySuccessful;

        public ParryState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Enter()
        {
            parryTimer = parryDuration;
            isParrySuccessful = false;
            AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.AttackParry);
            Debug.Log("Parry initiated!");
        }

        public override void Update()
        {
            if (parryTimer > 0)
            {
                parryTimer -= Time.deltaTime;
            }
            else
            {
                CharacterEntity.CharacterState.SetWalkState();
            }
        }

        public override void Exit()
        {
            if (isParrySuccessful)
            {
                Debug.Log("Parry successful! Enemy attack canceled.");
            }
            else
            {
                Debug.Log("Parry failed.");
            }
        }
    }
}
