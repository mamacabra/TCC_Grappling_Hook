using Character.GrapplingHook;
using Character.Melee;
using UnityEngine;

namespace Character
{
    public struct CharacterEntity
    {
        public Character Character;
        public CharacterInput CharacterInput;
        public CharacterMesh CharacterMesh;
        public CharacterState CharacterState;
        public CharacterUI CharacterUI;
        public GravityHandler GravityHandler;

        public AttackMelee AttackMelee;
        public GrapplingHookWeapon GrapplingHookWeapon;

        public Rigidbody Rigidbody;
    }
}
