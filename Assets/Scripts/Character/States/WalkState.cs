using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class WalkState : CharacterState
    {
        private const float Speed = 5.0f;
        private readonly Character _character;

        public WalkState(Character character)
        {
            _character = character;
        }

        public override void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _character.transform.position += _character.transform.forward * Speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _character.transform.position -= _character.transform.forward * Speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _character.transform.position -= _character.transform.right * Speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _character.transform.position += _character.transform.right * Speed * Time.deltaTime;
            }
        }
    }
}
