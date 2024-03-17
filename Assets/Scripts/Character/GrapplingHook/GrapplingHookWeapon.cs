using Character.Utils;
using UnityEngine;

namespace Character.GrapplingHook
{
    public class GrapplingHookWeapon : MonoBehaviour
    {
        private CharacterEntity _characterEntity;

        public int Force { get; private set; } = 1;
        public const int MaxGrapplingHookForce = 4;
        public const int DefaultGrapplingHookForce = 1;

        private bool _isHookDispatch;
        private bool _isHookRollback;

        private float _hookSpeed;
        private float _hookMaxDistance;

        private Vector3 _hookDirection;
        private Vector3 _hookOriginPosition;
        private Rigidbody _hookRigidbody;

        public void FixedUpdate()
        {
            if (_isHookDispatch is false && _isHookRollback is false) return;

            if (_isHookDispatch || _isHookRollback)
                _hookRigidbody.MovePosition(_hookRigidbody.transform.position + _hookDirection * (Time.fixedDeltaTime * _hookSpeed));

            var hookDistance = Vector3.Distance(_hookOriginPosition, _hookRigidbody.transform.position);
            if (_isHookDispatch && hookDistance >= _hookMaxDistance)
                RollbackHook();
            else if (_isHookRollback && hookDistance <= 0.1f)
                ResetHook();
        }

        public void Setup(CharacterEntity entity)
        {
            _characterEntity = entity;
        }

        public void DispatchHook(Vector3 direction)
        {
            if (_hookRigidbody is null) return;

            SetStats();
            _isHookDispatch = true;
            _hookDirection = direction;
            _hookOriginPosition = _hookRigidbody.transform.position;
        }

        private void RollbackHook()
        {
            if (_hookRigidbody is null) return;

            _isHookDispatch = false;
            _isHookRollback = true;
            _hookDirection *= -1;
        }

        private void ResetHook()
        {
            _isHookDispatch = false;
            _isHookRollback = false;
            _hookRigidbody.transform.position = _hookOriginPosition;
        }

        public void IncreaseForce()
        {
            Force++;
            _characterEntity.CharacterUI.UpdateForceUI(Force);
        }

        public void ResetForce()
        {
            Force = DefaultGrapplingHookForce;
            _characterEntity.CharacterUI.UpdateForceUI(Force);
        }

        private void SetStats()
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
