using Character.GrapplingHook;
using UnityEngine;

namespace Character.Utils
{
    public abstract class ACharacterMonoBehaviour : MonoBehaviour
    {
        public CharacterEntity CharacterEntity;

        public void Setup(CharacterEntity entity)
        {
            CharacterEntity = entity;
        }
    }
}
