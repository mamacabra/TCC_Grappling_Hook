using Character.GrapplingHook;
using UnityEngine;

namespace Character.Utils
{
    public abstract class ACharacterMonoBehaviour : MonoBehaviour
    {
        protected CharacterEntity CharacterEntity;
        protected GrapplingEntity GrapplingEntity;

        public void Setup(CharacterEntity entity, GrapplingEntity grapplingEntity)
        {
            CharacterEntity = entity;
            GrapplingEntity = grapplingEntity;
        }
    }
}
