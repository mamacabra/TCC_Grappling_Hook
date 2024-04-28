using Character.GrapplingHook;
using UnityEngine;

namespace Character
{
    public struct CharacterEntity
    {
        public bool IsDebug;
        public Character Character;
        public CharacterInput CharacterInput;
        public CharacterMesh CharacterMesh;
        public CharacterState CharacterState;
        public CharacterUI CharacterUI;
        public GrapplingHookWeapon GrapplingHookWeapon;
        public Rigidbody Rigidbody;
    }
}
