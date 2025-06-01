using Assets.Game.Code.Static;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Code.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Button _continueRunButton;
        [SerializeField] private GameObject _continueRunScreen;

        private void Start()
        {
            _continueRunButton.onClick.AddListener(() => RunButtonAction());
            Observer.Instance.OnReadyAimHandler += AimScreen;
        }

        private void AimScreen()
        {

        }

        private void RunButtonAction()
        {
            Observer.Instance.OnReadyRunHandler?.Invoke();
            _continueRunScreen.SetActive(false);
        }

        private void OnDestroy()
        {
            Observer.Instance.OnReadyAimHandler -= AimScreen;
        }
    }
}