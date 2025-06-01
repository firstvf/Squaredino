using Assets.Game.Code.Static;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Code.Game
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeed, _lifeTime;
        [SerializeField] private int _minDamage, _maxDamage;
        private WaitForSeconds _lifeTimeTimer;
        private bool _isAbleMovement;
        private Vector3 _movementDirection;
        private Coroutine _disableCoroutine;

        private void Awake()
        {
            _lifeTimeTimer = new(_lifeTime);
        }

        private void Update() => Movement();

        public void SetPath(Vector3 startPosition, Vector3 direction)
        {
            transform.position = startPosition;
            _movementDirection = direction;
            _isAbleMovement = true;

            _disableCoroutine = StartCoroutine(LifeTimeCoroutine());
        }

        private void Movement()
        {
            if (_isAbleMovement)
                transform.position += _bulletSpeed * Time.deltaTime * _movementDirection;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out Enemy enemy))
            {
                var randomDamage = Random.Range(_minDamage, _maxDamage);
                enemy.OnHit(randomDamage, _movementDirection);
                Observer.Instance.OnEnemyHitHandler?.Invoke(transform.position, _movementDirection);
            }

            gameObject.SetActive(false);
        }

        private IEnumerator LifeTimeCoroutine()
        {
            yield return _lifeTimeTimer;
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            if (_disableCoroutine != null)
                StopCoroutine(_disableCoroutine);

            _isAbleMovement = false;
        }
    }
}