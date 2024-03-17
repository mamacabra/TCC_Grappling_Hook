using UnityEngine;
using Character.GrapplingHook;
using Character.Utils;

namespace Character
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(CharacterUI))]
    [RequireComponent(typeof(GrapplingHookWeapon))]
    public class CharacterSetup : MonoBehaviour
    {
        private void Awake()
        {
            var character = gameObject.GetComponent<Character>();
            var grapplingHookUI = gameObject.GetComponent<CharacterUI>();
            var grapplingHookWeapon = gameObject.GetComponent<GrapplingHookWeapon>();

            var entity = new CharacterEntity
            {
                Character = character,
                CharacterUI = grapplingHookUI,
                GrapplingHookWeapon = grapplingHookWeapon,
            };

            grapplingHookWeapon.Setup(entity);
            character.Setup(entity);
        }
    }
}
