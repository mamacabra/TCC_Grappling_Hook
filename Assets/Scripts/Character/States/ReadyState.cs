using Character.Utils;

namespace Character.States
{
    public class ReadyState : ACharacterState
    {
        public ReadyState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter() {
            CharacterEntity.CharacterVFX.PlaySpawnVFXWithDelay(1.4f);
            CharacterEntity.GrapplingHookRope.SetActive(false);
        }

        public override void FixedUpdate()
        {
            LookAt();
        }
    }
}
