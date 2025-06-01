using Assets.Game.Code.Static;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Game.Code.UI
{
    public class AimWindow : MonoBehaviour, IPointerClickHandler
    {
        private Camera _camera;

        private void Start() => _camera = Camera.main;

        public void OnPointerClick(PointerEventData eventData)
        {
            Vector3 screenPoint = eventData.position;
            Ray ray = _camera.ScreenPointToRay(screenPoint);
            Vector3 targetPoint;

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, Observer.Instance.AimWindowSettings.GetLayer()))
                targetPoint = hit.point;
            else targetPoint = ray.GetPoint(100f);

            Vector3 direction = (targetPoint - Observer.Instance.AimWindowSettings.GetShotPoint()).normalized;
            Observer.Instance.OnTouchAimWindowHandler?.Invoke(direction);
        }
    }
}