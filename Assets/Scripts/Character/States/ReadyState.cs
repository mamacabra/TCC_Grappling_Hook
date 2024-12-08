using Character.Utils;

namespace Character.States
{
    public class ReadyState : ACharacterState
    {
        public ReadyState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter() {
            
            CharacterEntity.GrapplingHookRope.SetActive(false);
        }

        public override void FixedUpdate()
        {
            LookAt();
        }
    }
}
