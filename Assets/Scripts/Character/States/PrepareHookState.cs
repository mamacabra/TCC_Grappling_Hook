using UnityEngine;
using Character.Utils;

namespace Character.States
{
    public class PrepareHookState : ACharacterState
    {
        private float countDown;
        private const float CountDownStep = 0.2f;
        private const float WalkSpeed = 8f;
        private const float RotationSpeed = 5f;

        public PrepareHookState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.Hook.ResetHookForce();
            CharacterEntity.CharacterMesh.animator?.SetBool("isHook", true);
            AudioManager.audioManager.HookCharge();
        }

        public override void Update()
        {
            Walk(WalkSpeed);
            LookAt(RotationSpeed);
            CharacterEntity.Character.MoveArrowForward(3f,6f);
        }

        public override void FixedUpdate() {
            countDown += Time.fixedDeltaTime;

            if (countDown > CountDownStep)
            {
                countDown = 0f;
                CharacterEntity.Hook.IncreaseHookForce();
            }
        }
    }
}
