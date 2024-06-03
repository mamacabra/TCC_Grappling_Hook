using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class DeathState : ACharacterState
    {
        public DeathState(CharacterEntity characterEntity) : base(characterEntity) { }

        public override void Enter()
        {
            // Object.Destroy(CharacterEntity.Character.gameObject);

            CharacterEntity.AttackMelee.DisableHitbox();
            CharacterEntity.CharacterMesh.animator?.SetTrigger("isDead");
            CharacterEntity.GrapplingHookState.SetHookDestroyedState();

            CharacterEntity.GrapplingHookRope.SetActive(false);
            CharacterEntity.GrapplingHookRopeMuzzle.SetActive(false);
            CharacterEntity.GrapplingHookTransform.gameObject.SetActive(false);

            CharacterEntity.Character.transform.Find("Body/MeshParent/Sushi_Model(Clone)/DirectionPointer")?.gameObject.SetActive(false);
        }
    }
}
