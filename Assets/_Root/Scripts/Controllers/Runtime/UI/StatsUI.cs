using _Root.Scripts.Controllers.Runtime.Main;
using _Root.Scripts.Datas.Runtime.Statistics;
using Pancake;
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
            mainCharacterRef.Health.OnHealthChanged += UpdateHealthBar;
            mainCharacterRef.Health.OnMaxHealthChanged += UpdateHealthBarWidth;
            _healthBarRect = healthBar.GetComponent<RectTransform>();
            UpdateHealthBar();
            UpdateHealthBarWidth();
        }

        private void UpdateHealthBarWidth()
        {
            var size = _healthBarRect.sizeDelta;
            var maxHealth = mainCharacterRef.Health.MaxHealth;
            var barSize = healthBarRange.y - healthBarRange.x;
            var newSize = healthBarRange.x + Mathf.Log(maxHealth + 1, healthBarRange.x) * barSize - barSize;
            _healthBarRect.sizeDelta = new Vector2(newSize, size.y);
        }

        private void OnDisable()
        {
            mainCharacterRef.Health.OnHealthChanged -= UpdateHealthBar;
        }

        private void UpdateHealthBar()
        {
            healthBar.Value = mainCharacterRef.Health.CurrentHealth / mainCharacterRef.Health.MaxHealth;
        }
    }
}