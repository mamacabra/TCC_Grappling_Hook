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

        public AttackMelee AttackMelee;


        public GameObject GrapplingHookRope;
        public GameObject GrapplingHookRopeMuzzle;
        public BoxCollider GrapplingHookCollider;
        public GrapplingHookState GrapplingHookState;
        public Transform GrapplingHookTransform;

        public Rigidbody Rigidbody;
    }
}
