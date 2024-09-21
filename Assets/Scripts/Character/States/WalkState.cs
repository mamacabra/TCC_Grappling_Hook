using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class WalkState : ACharacterState
    {
        public WalkState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.GrapplingHookState.SetHookReadyState();
        }

        public override void FixedUpdate()
        {
            Walk();
            LookAt();
            CharacterEntity.Character.MoveArrowForward(speed: 10f);
        }
    }
}
