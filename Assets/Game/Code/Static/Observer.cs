using Assets.Game.Code.Game;
using Assets.Game.Code.Game.Level;
using System;
using UnityEngine;

namespace Assets.Game.Code.Static
{
    public class Observer : MonoBehaviour
    {
        public static Observer Instance { get; private set; }
        public ControlPointsProvider MovementProvider { get; private set; }
        public Action OnMoventPointReachedHandler { get; set; }
        public Action OnReadyRunHandler { get; set; }
        public Action OnReadyAimHandler { get; set; }
        public Action<Vector3> OnWeaponShotHandler { get; set; }
        public Action<Vector3, Vector3> OnEnemyHitHandler { get; set; }
        public Action OnPathCompleteHandler { get; set; }
        public Action<Vector3> OnTouchAimWindowHandler { get; set; }
        [field: SerializeField] public AimWindowSettings AimWindowSettings { get; private set; }
        [field: SerializeField] public Player Player { get; private set; }
        [field: SerializeField] public Transform BulletsContainer { get; private set; }

        private void Awake()
        {
            Init();
            MovementProvider = GetComponent<ControlPointsProvider>();
        }

        private void Init()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }

            Destroy(gameObject);
        }
    }
}