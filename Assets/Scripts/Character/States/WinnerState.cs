using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class WinnerState : ACharacterState
    {
        public WinnerState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.CharacterMesh.animator?.SetBool("isWinner", true);
            CharacterEntity.GrapplingHookTransform.gameObject.SetActive(false);
            CharacterEntity.Character.characterBody.LookAt(Vector3.back * 1000);
        }
    }
}
