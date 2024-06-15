using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class DashState : ACharacterState
    {
        private float countDown;
        private const float DashSpeed = 70f;
        private const float DashDuration = 0.12f;
        private ParticleSystem dashVfx;

        public DashState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.Character.UseDash();
            CharacterEntity.CharacterMesh.animator?.SetFloat("Speed", 0);
            CharacterEntity.CharacterMesh.animator?.SetBool("isDash", true);
            dashVfx = CharacterEntity.CharacterMesh.animator?.transform.Find("vfx.Dash").GetComponent<ParticleSystem>();
            dashVfx?.Play();
        }

        public override void FixedUpdate()
        {
            Walk(DashSpeed, true);

            countDown += Time.fixedDeltaTime;
            if (countDown > DashDuration)
            {
                CharacterEntity.CharacterState.SetWalkState();
            }
        }

        public override void Exit()
        {
            CharacterEntity.CharacterMesh.animator?.SetBool("isDash", false);
            dashVfx?.Stop();
        }
    }
}
