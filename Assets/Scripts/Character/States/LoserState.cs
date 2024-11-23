using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class LoserState : ACharacterState
    {
        Transform m_killedBy;

        public LoserState(CharacterEntity characterEntity, Transform killefBy) : base(characterEntity)
        {
            m_killedBy = killefBy;
        }

        public override void Enter()
        {
            CharacterEntity.GrapplingHookState.SetHookReadyState();
            CharacterEntity.CharacterMesh.ActiveDeath(m_killedBy);
        }
    }
}
