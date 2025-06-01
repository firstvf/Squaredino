using Assets.Game.Code.Data;
using Assets.Game.Code.Static;
using DG.Tweening;
using UnityEngine;

namespace Assets.Game.Code.Game
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private CameraParams _params;
        private Transform _target;
        private Vector3 _currentVelocity;
        private float _currentYaw;
        private bool _isAbleToFollowTarget;
        private Camera _camera;

        private void Awake() => _camera = Camera.main;

        private void Start()
        {
            _target = Observer.Instance.Player.transform;
            Observer.Instance.OnReadyAimHandler += DisableFollow;
            Observer.Instance.OnReadyRunHandler += EnableFollow;
            Observer.Instance.OnWeaponShotHandler += Shake;
        }

        private void LateUpdate() => MoveCamera();

        private void EnableFollow() => _isAbleToFollowTarget = true;
        private void DisableFollow() => _isAbleToFollowTarget = false;

        private void Shake(Vector3 direcion)
        {
            if (_params.CameraShake)
                _camera.transform.DOShakePosition(0.1f, strength: 0.1f);
        }

        private void MoveCamera()
        {
            if (_isAbleToFollowTarget == false)
                return;

            float targetYaw = _target.eulerAngles.y;

            _currentYaw = Mathf.LerpAngle(_currentYaw, targetYaw, Time.deltaTime * _params.RotationLagSpeed);
            Vector3 offset = Quaternion.Euler(0f, _currentYaw, 0f) * new Vector3(0f, 0f, -_params.Distance);
            Vector3 desiredPosition = _target.position + offset + Vector3.up * _params.Height;

            _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, desiredPosition, ref _currentVelocity, _params.PositionSmoothSpeed);
            _camera.transform.LookAt(_target.position + _params.Height * 0.5f * Vector3.up);
        }

        private void OnDestroy()
        {
            Observer.Instance.OnReadyAimHandler -= DisableFollow;
            Observer.Instance.OnReadyRunHandler -= EnableFollow;
            Observer.Instance.OnWeaponShotHandler -= Shake;
        }
    }
}