using _Root.Scripts.Controllers.Runtime.Main;
using _Root.Scripts.Datas.Runtime.Statistics;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;
using UnityProgressBar;

namespace _Root.Scripts.Controllers.Runtime.UI
{
    public class StatsUI : GameComponent
    {
        public MainCharacterRef mainCharacterRef;
        public ProgressBar healthBar;
        public Vector2 healthBarRange = new(Health.DefaultStaring, 400);
        public ProgressBar manaBar;

        private RectTransform _healthBarRect;

        private void OnEnable()
        {
            FloatVariable current = mainCharacterRef.Health.Current;
            current.OnValueChanged += UpdateHealthBar;
            UpdateHealthBar(current);
            
            mainCharacterRef.Health.OnMaxHealthChanged += UpdateHealthBarWidth;
            _healthBarRect = healthBar.GetComponent<RectTransform>();
            UpdateHealthBarWidth(current.Max);
        }

        private void UpdateHealthBarWidth(float maxHealth)
        {
            var size = _healthBarRect.sizeDelta;
            var barSize = healthBarRange.y - healthBarRange.x;
            var newSize = healthBarRange.x + Mathf.Log(maxHealth + 1, healthBarRange.x) * barSize - barSize;
            _healthBarRect.sizeDelta = new Vector2(newSize, size.y);
        }

        private void UpdateHealthBar(float obj)
        {
            healthBar.Value = obj / mainCharacterRef.Health.MaxHealth;
        }

        private void OnDisable()
        {
            mainCharacterRef.Health.Current.OnValueChanged -= UpdateHealthBar;
            mainCharacterRef.Health.OnMaxHealthChanged -= UpdateHealthBarWidth;
        }
    }
}