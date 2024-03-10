using UnityEngine;
using Character.GrapplingHook;
using Character.Utils;

namespace Character
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(GrapplingHookUI))]
    [RequireComponent(typeof(GrapplingHookWeapon))]
    public class CharacterSetup : MonoBehaviour
    {
        private void Awake()
        {
            var character = gameObject.GetComponent<Character>();
            var grapplingHookUI = gameObject.GetComponent<GrapplingHookUI>();
            var grapplingHookWeapon = gameObject.GetComponent<GrapplingHookWeapon>();

            var entity = new CharacterEntity
            {
                character = character,
                grapplingHookUI = grapplingHookUI,
                grapplingHookWeapon = grapplingHookWeapon,
            };

            grapplingHookWeapon.Setup(entity);
            character.Setup(entity);
        }
    }
}
