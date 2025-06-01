using Assets.Game.Code.Static;
using UnityEngine;

namespace Assets.Game.Code.Game
{
    public class AimWindowSettings : MonoBehaviour
    {
        [SerializeField] private LayerMask _shotMask;

        public Vector3 GetShotPoint() => Observer.Instance.Player.ShotPoint.position;
        public LayerMask GetLayer() => _shotMask;
    }
}