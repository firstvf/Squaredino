using Assets.Game.Code.Data;
using Assets.Game.Code.Static;
using UnityEngine;

namespace Assets.Game.Code.Game.Props
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletParams _params;
        private Vector3 _movementDirection;
        private Vector3 _startPosition;

        private void Update() => Movement();

        public void SetPath(Vector3 startPosition, Vector3 direction)
        {
            transform.position = startPosition;
            _startPosition = startPosition;
            _movementDirection = direction;
        }

        private void Movement()
        {
            transform.position += _params.Speed * Time.deltaTime * _movementDirection;

            float pathDistance = Vector3.Distance(_startPosition, transform.position);

            if (pathDistance >= _params.MaxDistance)
                gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out Enemy enemy))
            {
                var randomDamage = Random.Range(_params.MinDamage, _params.MaxDamage);
                enemy.OnHit(randomDamage, _movementDirection);
                Observer.Instance.OnEnemyHitHandler?.Invoke(transform.position, _movementDirection);
            }

            gameObject.SetActive(false);
        }
    }
}