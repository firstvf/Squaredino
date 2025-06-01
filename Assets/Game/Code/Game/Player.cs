using Assets.Game.Code.Data;
using Assets.Game.Code.Static;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Game.Code.Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _model;
        [SerializeField] private PlayerParams _params;
        public bool IsAiming { get; private set; }
        private Animator _animator;
        private NavMeshAgent _agent;
        private bool _isRunning;
        private Vector3 _targetPosition;
        private readonly int _idleAnimation = Animator.StringToHash("Idle");
        private readonly int _runAnimation = Animator.StringToHash("Run");
        private readonly int _aimAnimation = Animator.StringToHash("Aim");

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            Observer.Instance.OnReadyRunHandler += SetMovement;
            Observer.Instance.OnReadyAimHandler += SetAim;
            _agent.angularSpeed = _params.RotationSpeed;
            _agent.speed = _params.Speed;
            _agent.acceleration = _params.Acceleration;
        }

        private void Update()
        {
            if (_isRunning && IsReached())
            {
                _isRunning = false;
                _animator.SetTrigger(_idleAnimation);
                Observer.Instance.OnMoventPointReachedHandler?.Invoke();
            }
        }

        public void SetRotate(Vector3 direction)
        {
            Vector3 lookDirection = new(direction.x, 0f, direction.z);
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            _model.rotation = targetRotation;
        }

        private void SetAim()
        {
            _animator.SetTrigger(_aimAnimation);
            IsAiming = true;
        }

        private void SetMovement()
        {
            IsAiming = false;
            _isRunning = true;
            _targetPosition = Observer.Instance.MovementProvider.GetMovementPosition();
            _agent.SetDestination(_targetPosition);
            _animator.SetTrigger(_runAnimation);
        }

        private bool IsReached()
        {
            float sqrDistance = (transform.position - new Vector3(_targetPosition.x, transform.position.y, _targetPosition.z)).sqrMagnitude;
            return sqrDistance <= 0.1f;
        }

        private void OnDestroy()
        {
            Observer.Instance.OnReadyRunHandler -= SetMovement;
            Observer.Instance.OnReadyAimHandler -= SetAim;
        }
    }
}