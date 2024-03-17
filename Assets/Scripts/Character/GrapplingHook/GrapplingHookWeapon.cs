using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook
{
    public class GrapplingHookWeapon : MonoBehaviour
    {
        public int Force { get; private set; } = 1;
        public const int MaxGrapplingHookForce = 4;
        public const int DefaultGrapplingHookForce = 1;

        [Header("Character Entity")]
        [SerializeField] private GrapplingHookUI grapplingHookUI;

        [Header("Grappling Hook")]
        [SerializeField] private bool isHookDispatch;
        [SerializeField] private bool isHookRollback;
        [SerializeField] private float hookSpeed;
        [SerializeField] private float hookMaxDistance;
        [SerializeField] private Rigidbody hookRigidbody;
        private Vector3 _hookOriginPosition;
        private Vector3 _hookDirection;
        private float _hookDistance;

        public void Start()
        {
            _hookOriginPosition = hookRigidbody.transform.position;
        }

        public void FixedUpdate()
        {
            if (isHookDispatch is false && isHookRollback is false) return;

            if (isHookDispatch || isHookRollback)
                hookRigidbody.MovePosition(hookRigidbody.transform.position + _hookDirection * (Time.deltaTime * hookSpeed));

            _hookDistance = Vector3.Distance(_hookOriginPosition, hookRigidbody.transform.position);
            if (isHookDispatch && _hookDistance >= hookMaxDistance)
                RollbackHook();
            else if (isHookRollback && _hookDistance <= 0.1f)
                ResetHook();
        }

        public void Setup(CharacterEntity entity)
        {
            grapplingHookUI = entity.grapplingHookUI;
        }

        public void DispatchHook()
        {
            if (hookRigidbody is null) return;

            SetStats();
            isHookDispatch = true;
            _hookDirection = transform.forward;
        }

        private void RollbackHook()
        {
            if (hookRigidbody is null) return;

            isHookDispatch = false;
            isHookRollback = true;
            _hookDirection = transform.forward * -1;
        }

        private void ResetHook()
        {
            isHookDispatch = false;
            isHookRollback = false;
            hookRigidbody.transform.position = _hookOriginPosition;
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

        private void SetStats()
        {
            switch (Force)
            {
                case 1:
                    hookSpeed = GrapplingStats.SpeedDefault;
                    hookMaxDistance = GrapplingStats.DistanceDefault;
                    break;
                case 2:
                    hookSpeed = GrapplingStats.SpeedMedium;
                    hookMaxDistance = GrapplingStats.DistanceDefault;
                    break;
                case 3:
                    hookSpeed = GrapplingStats.SpeedMedium;
                    hookMaxDistance = GrapplingStats.DistanceFar;
                    break;
                default:
                    hookSpeed = GrapplingStats.SpeedFast;
                    hookMaxDistance = GrapplingStats.DistanceFar;
                    break;
            }
        }
    }
}
