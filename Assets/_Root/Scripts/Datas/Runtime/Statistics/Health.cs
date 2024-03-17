using System;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [Serializable]
    public class Health : GameComponent, IHealth, IInvulnerable, IDeath
    {
        public const float DefaultStaring = 100;

#if UNITY_EDITOR
        private float _previousMaxHealth;
#endif

        #region IHealth

        [Tooltip("the current health of the character")]
        [field: SerializeReference]
        public float CurrentHealth { get; private set; } = DefaultStaring;

        [Tooltip("the maximum health of the character")]
        [field: SerializeReference]
        public float MaxHealth { get; private set; } = 100;

        public event Action OnHealthChanged;
        public event Action OnMaxHealthChanged;

        public bool increaseHealthWithMaxHealth = true;

        public float SetHealth(float value)
        {
            var newHealth = Mathf.Clamp(value, 0, MaxHealth);
#if !UNITY_EDITOR
            if (Mathf.Approximately(newHealth, CurrentHealth)) return CurrentHealth;
#endif
            OnHealthChanged?.Invoke();
            return CurrentHealth = newHealth;
        }

        public float SetMaxHealth(float value)
        {
            float newMax = Mathf.Max(value, 0);
#if !UNITY_EDITOR
            if (Mathf.Approximately(newMax, MaxHealth)) return MaxHealth;
#endif
            MaxHealth = newMax;
#if !UNITY_EDITOR
            if (increaseHealthWithMaxHealth) SetHealth(CurrentHealth + newMax - MaxHealth);
#else 
            if (increaseHealthWithMaxHealth) SetHealth(CurrentHealth + newMax - _previousMaxHealth);
            _previousMaxHealth = MaxHealth;
#endif
            OnMaxHealthChanged?.Invoke();
            return MaxHealth;
        }

        #endregion

        #region IDeath

        [Tooltip("If this is true, this object can't take damage at this time")]
        [field: SerializeReference]
        public bool Invulnerable { get; set; }


        [Tooltip("if this is true, the character is dead")]
        [field: SerializeReference]
        public bool IsDead { get; private set; }

        public event Action OnDeath;
        public event Action OnRevive;

        public void Die()
        {
            IsDead = true;
            OnDeath?.Invoke();
        }

        public void Revive(float health = 1)
        {
            IsDead = false;
            SetHealth(health);
            OnRevive?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            if (Invulnerable) return;
            SetHealth(CurrentHealth - damage);
            if (CurrentHealth <= 0) Die();
        }

        #endregion


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (IsDead) Die();
            SetHealth(CurrentHealth);
            Debug.Log("Health: " + CurrentHealth);
            SetMaxHealth(MaxHealth);
        }
#endif
    }
}