using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class DeathState : ACharacterState
    {
        public DeathState(CharacterEntity characterEntity, Transform killefBy) : base(characterEntity) { m_killedBy = killefBy; }
        Transform m_killedBy;
        public override void Enter()
        {
            AudioManager.audioManager.PlayPlayerSoundEffect(PlayerSoundsList.AttackHitPlayer);

            // Object.Destroy(CharacterEntity.Character.gameObject);
            CharacterEntity.Character.tag = "Untagged";
            CharacterEntity.CharacterCollider.enabled = false;

            CharacterEntity.AttackMelee.DisableHitbox();
            CharacterEntity.CharacterMesh.ActiveDeath(m_killedBy);
            CharacterEntity.GrapplingHookState.SetHookDestroyedState();

            CharacterEntity.GrapplingHookRope.SetActive(false);
            CharacterEntity.GrapplingHookRopeMuzzle.SetActive(false);
            CharacterEntity.GrapplingHookTransform.gameObject.SetActive(false);

            CharacterEntity.Character.transform.Find("Body/MeshParent/Sushi_Model(Clone)/DirectionPointer")?.gameObject.SetActive(false);
        }
    }
}
