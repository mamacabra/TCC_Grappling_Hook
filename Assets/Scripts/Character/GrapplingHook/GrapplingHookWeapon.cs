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

        public void Update()
        {
            _hookDistance = Vector3.Distance(_hookOriginPosition, hookRigidbody.transform.position);

            if (_hookDistance >= hookMaxDistance)
            {
                RollbackHook();
                Debug.Log("ROLLBACK");
            }
        }

        public void FixedUpdate()
        {
            if (isHookDispatch == false) return;

            if (isHookDispatch || isHookRollback)
            {
                hookRigidbody.MovePosition(hookRigidbody.transform.position + _hookDirection * (Time.deltaTime * hookSpeed));
            }

            // var hookOriginDistance = Vector3.Dot(_hookOriginPosition, hookRigidbody.transform.position);
            // if (hookOriginDistance <= 0 && isHookRollback)
            // {
            // //     // hookRigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * m_Speed);
            // //     hookRigidbody.velocity = Vector3.zero;
            // //     hookRigidbody.transform.position = _hookOriginPosition;
            //     isHookDispatch = false;
            //     isHookRollback = false;
            // }
        }

        public void Setup(CharacterEntity entity)
        {
            grapplingHookUI = entity.grapplingHookUI;
        }

        public void DispatchHook()
        {
            if (!hookRigidbody) return;

            SetStats();
            _hookDirection = transform.forward;
            isHookDispatch = true;
        }

        public void RollbackHook()
        {
            if (!hookRigidbody) return;

            // hookSpeed = GrapplingStats.SpeedRollback;
            // hookRigidbody.velocity = direction * hookSpeed;
            _hookDirection = transform.forward * -1;
            isHookDispatch = false;
            isHookRollback = true;
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
