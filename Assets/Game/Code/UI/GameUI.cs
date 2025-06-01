using Assets.Game.Code.Static;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Game.Code.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _infoWindowText;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _infoWindow;

        private void Start()
        {
            _button.onClick.AddListener(() => RunButtonAction());
            Observer.Instance.OnPathCompleteHandler += EnableInfoWindow;
        }

        private void EnableInfoWindow() => _infoWindow.SetActive(true);
        private void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        private void RunButtonAction()
        {
            Observer.Instance.OnReadyRunHandler?.Invoke();
            _infoWindow.SetActive(false);
            _button.onClick.AddListener(() => Restart());
            _infoWindowText.text = "TAP TO RESTART GAME";
        }

        private void OnDestroy()
        {
            Observer.Instance.OnPathCompleteHandler -= EnableInfoWindow;
        }
    }
}