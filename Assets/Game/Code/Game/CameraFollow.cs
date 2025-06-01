using Assets.Game.Code.Static;
using UnityEngine;

namespace Assets.Game.Code.Game
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _height = 3f;
        [SerializeField] private float _distance = 6f;
        [SerializeField] private float _rotationLagSpeed = 5f;
        [SerializeField] private float _positionSmoothSpeed = 0.1f;
        private Vector3 _currentVelocity;
        private float _currentYaw;
        private bool _isAbleToFollowTarget;
        private Camera _camera;

        private void Awake() => _camera = Camera.main;

        private void Start()
        {
            Observer.Instance.OnReadyAimHandler += DisableFollow;
            Observer.Instance.OnReadyRunHandler += EnableFollow;
        }

        private void LateUpdate() => MoveCamera();

        private void EnableFollow() => _isAbleToFollowTarget = true;
        private void DisableFollow() => _isAbleToFollowTarget = false;

        private void MoveCamera()
        {
            if (_isAbleToFollowTarget == false)
                return;

            float targetYaw = _target.eulerAngles.y;

            _currentYaw = Mathf.LerpAngle(_currentYaw, targetYaw, Time.deltaTime * _rotationLagSpeed);
            Vector3 offset = Quaternion.Euler(0f, _currentYaw, 0f) * new Vector3(0f, 0f, -_distance);
            Vector3 desiredPosition = _target.position + offset + Vector3.up * _height;

            _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, desiredPosition, ref _currentVelocity, _positionSmoothSpeed);
            _camera.transform.LookAt(_target.position + _height * 0.5f * Vector3.up);
        }

        private void OnDestroy()
        {
            Observer.Instance.OnReadyAimHandler -= DisableFollow;
            Observer.Instance.OnReadyRunHandler -= EnableFollow;
        }
    }
}