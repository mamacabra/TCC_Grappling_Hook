using UnityEngine;

namespace Character.Utils
{
    public abstract class ACharacterMonoBehaviour : MonoBehaviour
    {
        protected CharacterEntity CharacterEntity;

        public void Setup(CharacterEntity entity)
        {
            CharacterEntity = entity;
        }
    }
}
