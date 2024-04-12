using Character.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Character.States
{
    public class MeleeState : ACharacterState
    {
        public MeleeState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Update()
        {
            if(CharacterEntity.CharacterRaycast.EnemyHit == true)
            {

            }
        }





    }

}
