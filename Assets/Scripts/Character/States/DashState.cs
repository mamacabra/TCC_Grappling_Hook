using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class DashState : ACharacterState
    {
        private float countDown;
        private const float DashSpeed = 70f;
        private const float DashDuration = 0.2f;

        public DashState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.Character.UseDash();
            CharacterEntity.CharacterMesh.animator?.SetFloat("Speed", 1);
        }

        public override void FixedUpdate()
        {
            Walk(DashSpeed, true);

            countDown += Time.fixedDeltaTime;
            if (countDown > DashDuration)
            {
                CharacterEntity.CharacterState.SetWalkState();
            }
        }
    }
}
