using Assets.Game.Code.Data;
using Assets.Game.Code.Game.Level;
using UnityEngine;

namespace Assets.Game.Code.Game
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyParams _params;
        [SerializeField] private Rigidbody[] _ragdollComponents;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider _collider;
        private int _maxHealth;
        private int _currentHealth;
        private ControlPoint _controlPoint;
        private HealthBar _healthBar;

        private void Awake() => _healthBar = GetComponent<HealthBar>();

        private void Start()
        {
            _maxHealth = _params.Health;
            _currentHealth = _maxHealth;
            _healthBar.RegisterHealthBar(_maxHealth);
            _controlPoint = GetComponentInParent<ControlPoint>();
            _controlPoint.AddEnemyToPoint(this);
        }

        public void OnHit(int damage, Vector3 hitDirection)
        {
            if (_currentHealth <= 0)
                return;

            _currentHealth -= damage;
            _healthBar.ChangeHealthBar(_currentHealth);

            if (_currentHealth <= 0)
                Death(hitDirection);
        }

        private void Death(Vector3 hitDirection)
        {
            ActivateRagdoll(hitDirection);
            _controlPoint.RemoveEnemyFromPoint(this);
        }

        private void ActivateRagdoll(Vector3 hitDirection)
        {
            _collider.enabled = false;
            _animator.enabled = false;

            for (int i = 0; i < _ragdollComponents.Length; i++)
            {
                _ragdollComponents[i].isKinematic = false;
                _ragdollComponents[i].AddForce(hitDirection.normalized * 20f, ForceMode.Impulse);
            }
        }
    }
}