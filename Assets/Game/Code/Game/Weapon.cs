using Assets.Game.Code.Game.Props;
using Assets.Game.Code.Static;
using Assets.Game.Code.Utils;
using UnityEngine;

namespace Assets.Game.Code.Game
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        private Transform _shootPoint;
        private Player _player;
        private ObjectPooler<Bullet> _bulletPooler;
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
            _player = Observer.Instance.Player;
            _shootPoint = _player.ShotPoint;
            _bulletPooler = new(_bullet, Observer.Instance.BulletsContainer, 5);
            Observer.Instance.OnTouchAimWindowHandler += ShotAction;
        }

        private void ShotAction(Vector3 direction)
        {
            if (_player.IsAiming == false)
                return;

            Observer.Instance.OnWeaponShotHandler?.Invoke(_shootPoint.position);
            _player.SetRotate(direction);
            var bullet = _bulletPooler.GetObject();
            bullet.SetPath(_shootPoint.position, direction);
        }

        private void OnDestroy()
        {
            Observer.Instance.OnTouchAimWindowHandler -= ShotAction;
        }
    }
}