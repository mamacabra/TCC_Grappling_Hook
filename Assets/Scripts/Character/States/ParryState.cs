using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class ParryState : ACharacterState
    {
        private float parryDuration = 1.0f;
        private float parryTimer;

        private Vector3 initialPosition;

        public ParryState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Enter()
        {
            initialPosition = CharacterEntity.Character.transform.position;
            parryTimer = parryDuration;
            CharacterEntity.CharacterVFX.PlayParryVFX();
            AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.AttackParry);
            Debug.Log("Parry");
            
        }

        public override void Update()
        {            
            if (parryTimer > 0)
            {
                parryTimer -= Time.deltaTime;
                CharacterEntity.Character.transform.position = initialPosition;
            }
            else
            {
                CharacterEntity.CharacterState.SetWalkState();
            }
        }

    }
}
