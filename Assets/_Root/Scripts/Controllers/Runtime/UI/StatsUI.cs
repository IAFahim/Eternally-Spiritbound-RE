using _Root.Scripts.Controllers.Runtime.Characters;
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
        public CharacterRef characterRef;

        [Header("Health")] [SerializeField] private ProgressBar healthBar;
        [SerializeField] private Vector2 healthBarRange = new(HealthBase.DefaultStaring, 400);
        [SerializeField] private RectTransform healthBarRect;

        [Header("Title")] [SerializeField] private ProgressBar manaBar;
        [SerializeField] private Vector2 manaBarRange = new(40, 100);


        private void OnValidate()
        {
            healthBarRect ??= healthBar.GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            characterRef.onCharacterUpdateEvent.OnRaised += OnCharacterUpdate;
            if (characterRef.main != null) OnCharacterUpdate(characterRef.main);
        }

        private void OnDisable()
        {
            characterRef.onCharacterUpdateEvent.OnRaised -= OnCharacterUpdate;
            RemoveOldListeners();
        }

        private void OnCharacterUpdate(Character mainCharacter)
        {
            RemoveOldListeners();
            AddNewListeners(mainCharacter);
        }

        private void AddNewListeners(Character mainCharacter)
        {
            var stats = mainCharacter.stats;
            AddListenerForHealth(mainCharacter.healthBase);
            AddListenerForMana(stats.manaCurrent, stats.manaMax);
        }

        private void AddListenerForHealth(HealthBase healthBase)
        {
            healthBase.OnMaxChanged += HelathBarWidthUpdate;
            healthBase.OnCurrentChanged += HealthBarFillUpdate;
            
            HelathBarWidthUpdate(healthBase.Max);
            HealthBarFillUpdate(healthBase.Current);
        }
        
        private void AddListenerForMana(FloatReference manaCurrent, FloatReference manaMax)
        {
            if (!manaCurrent.useLocal) manaCurrent.variable.OnValueChanged += UpdateBarMana;
            UpdateBarMana(manaCurrent);
            if (!manaMax.useLocal) manaMax.variable.OnValueChanged += UpdateBarMana;
            
        }
        
        private void RemoveOldListeners()
        {
            if (characterRef.main != null)
            {
                var character = characterRef.main;
                HealthListenerAllRemove(character.healthBase);
                ManaListenerAllRemove(character.stats.manaCurrent, character.stats.manaMax);
            }
        }

        private void HealthListenerAllRemove(HealthBase healthBase)
        {
            healthBase.OnCurrentChanged -= HealthBarFillUpdate;
            healthBase.OnMaxChanged -= HelathBarWidthUpdate;
        }

        private void ManaListenerAllRemove(FloatReference statsManaCurrent, FloatReference statsManaMax)
        {
            if (!statsManaCurrent.useLocal) statsManaCurrent.variable.OnValueChanged -= UpdateBarMana;
            if(!statsManaMax.useLocal) statsManaMax.variable.OnValueChanged -= UpdateBarMana;
        }


        private void HealthBarFillUpdate(float current)
        {
            healthBar.Value = current / characterRef.main.healthBase.Max;
        }

        private void UpdateBarMana(float current)
        {
            manaBar.Value = current / characterRef.main.stats.manaMax;
        }

        private void HelathBarWidthUpdate(float maxHealth)
        {
            var size = healthBarRect.sizeDelta;
            var barSize = healthBarRange.y - healthBarRange.x;
            var newSize = healthBarRange.x + Mathf.Log(maxHealth + 1, healthBarRange.x) * barSize - barSize;
            healthBarRect.sizeDelta = new Vector2(newSize, size.y);
        }
    }
}