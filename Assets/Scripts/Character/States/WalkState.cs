using Character.Utils;

namespace Character.States
{
    public class WalkState : ACharacterState
    {
        public WalkState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.GrapplingHookWeapon.ResetHook();
        }

        public override void FixedUpdate()
        {
            Walk();
            LookAt();
        }
    }
}
