using Assets.Game.Code.Static;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Code.Game.Level
{
    public class ControlPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _pointer;
        private readonly List<Enemy> _enemyList = new();
        private Tween _pointerTween;
        private bool _isCurrentPoint;

        private void Start() => AnimatePointer();

        public void SwitchCurrentPoint(bool isCurrentPoint) => _isCurrentPoint = isCurrentPoint;

        public void AddEnemyToPoint(Enemy enemy)
        {
            if (_enemyList.Contains(enemy) == false)
                _enemyList.Add(enemy);
        }

        public void RemoveEnemyFromPoint(Enemy enemy)
        {
            _enemyList.Remove(enemy);

            if (_enemyList.Count <= 0 && _isCurrentPoint)
                Invoke(nameof(CallActionDelay), 0.5f);
        }

        public void VerifyPointStatus()
        {
            if (_enemyList.Count <= 0)
                Observer.Instance.OnReadyRunHandler?.Invoke();
            else Observer.Instance.OnReadyAimHandler?.Invoke();
        }

        private void CallActionDelay() => Observer.Instance.OnReadyRunHandler?.Invoke();

        private void AnimatePointer()
        {
            _pointerTween = _pointer.transform.DOLocalMoveY(1, 1f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
            _pointerTween.Kill();
        }
    }
}