using Character.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Character.States
{
    public class DeathState : ACharacterState
    {
        

        public DeathState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Update()
        {
            if (CharacterEntity.CharacterRaycast.HasHit == true)
            {
                
            }
        }

    }

}
