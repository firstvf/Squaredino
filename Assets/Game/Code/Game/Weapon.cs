using Assets.Game.Code.Game.Props;
using Assets.Game.Code.Static;
using Assets.Game.Code.Utils;
using UnityEngine;

namespace Assets.Game.Code.Game
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _bulletsContainer;
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _shootPoint;
        private Player _player;
        private ObjectPooler<Bullet> _bulletPooler;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _bulletPooler = new(_bullet, _bulletsContainer, 5);
        }

        private void Update()
        {
            if (_player.IsAiming && Input.GetMouseButtonDown(0))
            {
                ShootAtTouch();
            }
        }

        private void ShootAtTouch()
        {
            Vector3 screenPoint = Input.mousePosition;

            Ray ray = _mainCamera.ScreenPointToRay(screenPoint);
            Vector3 direction = ray.direction.normalized;

            Observer.Instance.OnWeaponShotHandler?.Invoke(_shootPoint.position);
            _player.SetRotate(direction);

            if (Physics.Raycast(ray, out RaycastHit hit))
                direction = (hit.point - _shootPoint.position).normalized;

            var bullet = _bulletPooler.GetObject();
            bullet.SetPath(_shootPoint.position, direction);
        }
    }
}