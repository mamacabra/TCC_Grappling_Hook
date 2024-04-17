using Character.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Character.States
{
    public class DeathState : ACharacterState
    {
        public DeathState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Enter()
        {
            Debug.Log("Dead");
            CharacterEntity.CharacterMesh.animator.SetTrigger("isDead");
            //CharacterEntity.Character.gameObject.SetActive(false);
        }

    }

}
