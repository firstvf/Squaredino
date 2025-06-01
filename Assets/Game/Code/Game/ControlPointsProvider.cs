using Assets.Game.Code.Static;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Code.Game
{
    public class ControlPointsProvider : MonoBehaviour
    {
        [SerializeField] private List<ControlPoint> _points;
        private int _currentPointId = 0;
        private bool _isFirstPointGet;

        private void Start()
        {
            Observer.Instance.OnMoventPointReachedHandler += VerifyPointStatus;
        }

        private void VerifyPointStatus()
        {
            _points[_currentPointId].VerifyPointStatus();
        }

        public Vector3 GetMovementPosition()
        {
            if (_isFirstPointGet == false)
                _isFirstPointGet = true;
            else _currentPointId++;

            if (_currentPointId >= _points.Count)
            {
                Debug.Log("Player reached last position");
                _currentPointId--;
                Observer.Instance.OnMoventPointReachedHandler -= VerifyPointStatus;
            }

            return _points[_currentPointId].transform.position;
        }
    }
}