using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Code.Game
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Canvas _worldCanvas;
        [SerializeField] private Slider _healthBar;
        [SerializeField] private Image _healthBarFill;
        private int _maxHealth;

        public void RegisterHealthBar(int health)
        {
            _maxHealth = health;
            _healthBar.maxValue = _maxHealth;
            _healthBar.value = _maxHealth;
        }

        public void ChangeHealthBar(int value)
        {
            if (value <= 0)
                _worldCanvas.gameObject.SetActive(false);
            else _worldCanvas.gameObject.SetActive(true);

            _healthBar.value = value;
            ChangeSliderColor(value);
        }

        private void ChangeSliderColor(float value)
        {
            if (value <= _maxHealth * 0.3f)
                _healthBarFill.color = Color.red;
            else if (value <= _maxHealth * 0.6f)
                _healthBarFill.color = Color.yellow;
        }
    }
}