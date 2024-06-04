using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.Utils;

namespace Character.States
{
    public class ParryState : ACharacterState
    {
        public ParryState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.AttackParry);
        }
    }
}

