using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class DeathState : ACharacterState
    {
        public bool isDead;

        public DeathState(CharacterEntity characterEntity) : base(characterEntity) { }
        

    }

}
