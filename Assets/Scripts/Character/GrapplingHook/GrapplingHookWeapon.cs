using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook
{
    public class GrapplingHookWeapon : MonoBehaviour
    {
        public const int MaxGrapplingHookForce = 4;
        public const int DefaultGrapplingHookForce = 1;

        public int Force { get; private set; } = 1;
        [SerializeField] private GameObject pointer;
        [SerializeField] private GrapplingHookUI grapplingHookUI;

        public void Setup(CharacterEntity entity)
        {
            grapplingHookUI = entity.grapplingHookUI;
        }

        public void Dispatch()
        {
        }

        public void IncreaseForce()
        {
            Force++;
            grapplingHookUI.UpdateForceUI(Force);
        }

        public void ResetForce()
        {
            Force = DefaultGrapplingHookForce;
            grapplingHookUI.UpdateForceUI(Force);
        }
    }
}
