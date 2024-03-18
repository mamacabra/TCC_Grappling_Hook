using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook
{
    public class GrapplingHookWeapon : ACharacterMonoBehaviour
    {
        private int _force = 1;
        public const int MaxGrapplingHookForce = 4;
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
        private readonly Vector3 _hookOriginLocalPosition = new Vector3(0.4f, 1, 0.7f);
        [SerializeField] private Rigidbody hookRigidbody;

        public void FixedUpdate()
        {
            if (_isHookDispatch is false && _isHookRollback is false) return;

            if (_isHookDispatch || _isHookRollback)
                hookRigidbody.MovePosition(hookRigidbody.transform.position + _hookDirection * (Time.fixedDeltaTime * _hookSpeed));

            var hookDistance = Vector3.Distance(_hookOriginLocalPosition, hookRigidbody.transform.localPosition);
            if (_isHookDispatch && hookDistance >= _hookMaxDistance)
                CharacterEntity.CharacterState.SetRollbackHookState();
            else if (_isHookRollback && hookDistance <= 0.1f)
                CharacterEntity.CharacterState.SetWalkState();
        }

        public new void Setup(CharacterEntity entity)
        {
            CharacterEntity = entity;
            hookRigidbody = transform.Find("GrapplingHook").GetComponent<Rigidbody>();
        }

        public void DispatchHook(Vector3 direction)
        {
            if (hookRigidbody is null) return;

            SetHookStats();
            _isHookDispatch = true;
            _isHookRollback = false;
            _hookDirection = direction;
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
                    _hookSpeed = GrapplingStats.SpeedDefault;
                    _hookMaxDistance = GrapplingStats.DistanceDefault;
                    break;
                case 2:
                    _hookSpeed = GrapplingStats.SpeedMedium;
                    _hookMaxDistance = GrapplingStats.DistanceDefault;
                    break;
                case 3:
                    _hookSpeed = GrapplingStats.SpeedMedium;
                    _hookMaxDistance = GrapplingStats.DistanceFar;
                    break;
                default:
                    _hookSpeed = GrapplingStats.SpeedFast;
                    _hookMaxDistance = GrapplingStats.DistanceFar;
                    break;
            }
        }
    }
}
