using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class WaitingState : ACharacterState
    {
        public WaitingState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {

        }

        public override void FixedUpdate()
        {
            LookAt();
        }
    }
}