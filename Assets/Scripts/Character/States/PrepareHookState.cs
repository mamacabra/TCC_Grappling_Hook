using UnityEngine;
using Character.Utils;

namespace Character.States
{
    public class PrepareHookState : ACharacterState
    {
        private float countDown;
        private const float CountDownStep = 0.1f;
        private const float WalkSpeed = 8f;

        public PrepareHookState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Update()
        {
            Walk(WalkSpeed);
            LookAt();
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
