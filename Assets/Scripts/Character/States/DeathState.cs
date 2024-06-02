using Character.Utils;

namespace Character.States
{
    public class DeathState : ACharacterState
    {
        public DeathState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Enter()
        {
            CharacterEntity.AttackMelee.DisableHitbox();
            CharacterEntity.CharacterMesh.animator?.SetTrigger("isDead");
            CharacterEntity.GrapplingHookState.SetHookDestroyedState();
        }
    }
}
