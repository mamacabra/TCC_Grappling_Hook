using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook
{
    public class GrapplingHookWeapon : ACharacterMonoBehaviour
    {
        [SerializeField] private Transform hook;

        private int _force = 1;
        public const int MaxGrapplingHookForce = 3;
        public const int DefaultGrapplingHookForce = 1;
        public int Force
        {
            get => _force;
            private set
            {
                _force = value;
                CharacterEntity.CharacterUI.UpdateForceUI(value);
            }
        }

        private bool _isHookDispatch;
        private bool _isHookRollback;

        private float _hookSpeed;
        private float _hookMaxDistance;

        private Vector3 _hookDirection;
        private readonly Vector3 _hookOriginLocalPosition = new (0f, 1, 1.2f);
        [SerializeField] private Rigidbody hookRigidbody;
        [SerializeField] private BoxCollider hookCollider;
        [SerializeField] private GrapplingHook grapplingHook;

        public void FixedUpdate()
        {
            if (_isHookDispatch || _isHookRollback)
                hookRigidbody.transform.Translate(_hookDirection * (Time.fixedDeltaTime * _hookSpeed));

            var hookDistance = Vector3.Distance(_hookOriginLocalPosition, hookRigidbody.transform.localPosition);
            if (_isHookDispatch && hookDistance >= _hookMaxDistance)
                CharacterEntity.CharacterState.SetRollbackHookState();
            else if (_isHookRollback && (hookDistance <= 0.1f || hookRigidbody.transform.localPosition.z <= _hookOriginLocalPosition.z))
                CharacterEntity.CharacterState.SetWalkState();
        }

        public new void Setup(CharacterEntity entity)
        {
            CharacterEntity = entity;

            // var hook = transform.Find("GrapplingHook");
            hookCollider = hook.GetComponent<BoxCollider>();
            hookRigidbody = hook.GetComponent<Rigidbody>();
            if (grapplingHook) grapplingHook.Setup(entity);

            DisableHook();
        }

        private void DisableHook()
        {
            hookCollider.enabled = false;
        }

        private void EnableHook()
        {
            hookCollider.enabled = true;
        }

        public void DispatchHook(Vector3 direction)
        {
            if (hookRigidbody is null) return;

            SetHookStats();
            _isHookDispatch = true;
            _isHookRollback = false;
            _hookDirection = direction;
            EnableHook();
        }

        public void AttachHook()
        {
            if (hookRigidbody is null) return;

            _isHookDispatch = false;
            _isHookRollback = false;
            DisableHook();
        }

        public void RollbackHook()
        {
            if (hookRigidbody is null) return;

            _isHookDispatch = false;
            _isHookRollback = true;
            _hookDirection *= -1;
        }

        public void ResetHook()
        {
            if (hookRigidbody is null) return;

            _isHookDispatch = false;
            _isHookRollback = false;
            Force = DefaultGrapplingHookForce;
            hookRigidbody.transform.localPosition = _hookOriginLocalPosition;
            DisableHook();
        }

        public void IncreaseHookForce()
        {
            Force++;
        }

        private void SetHookStats()
        {
            switch (Force)
            {
                case 1:
                    _hookSpeed = GrapplingStats.ForceLv1.speed;
                    _hookMaxDistance = GrapplingStats.ForceLv1.distance;
                    break;
                case 2:
                    _hookSpeed = GrapplingStats.ForceLv2.speed;
                    _hookMaxDistance = GrapplingStats.ForceLv2.distance;
                    break;
                default:
                    _hookSpeed = GrapplingStats.ForceLv3.speed;
                    _hookMaxDistance = GrapplingStats.ForceLv3.distance;
                    break;
            }
        }
    }
}
