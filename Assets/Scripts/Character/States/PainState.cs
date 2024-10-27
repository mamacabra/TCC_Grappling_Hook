using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class PainState : ACharacterState
    {
        private float countDown;
        private const float PenaltyDuration = 0.3f;

        public PainState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void FixedUpdate()
        {
            countDown += Time.fixedDeltaTime;
            if (countDown > PenaltyDuration)
            {
                CharacterEntity.CharacterState.SetWalkState();
            }
        }
    }
}
