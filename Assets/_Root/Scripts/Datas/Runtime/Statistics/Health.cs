using System;
using System.Collections;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [Serializable]
    public class Health : GameComponent, IHealth, IDamage, IDeath
    {
        public const float DefaultStaring = 100;

        [Tooltip("If this is true, this object can't take damage at this time")]
        [field: SerializeReference]
        public bool DamageAble { get; private set; }

        [Tooltip("the current health of the character")]
        [field: SerializeReference]
        public float CurrentHealth { get; private set; } = DefaultStaring;

        [Tooltip("the maximum health of the character")]
        [field: SerializeReference]
        public float MaxHealth { get; private set; } = 100;


        public bool increaseHealthWithMaxHealth = true;


        [Tooltip("if this is true, the character is dead")]
        [field: SerializeReference]
        public bool IsDead { get; private set; }

        [SerializeField] private DamageResistanceBase damageResistance;
        public event Action OnHealthChanged;
        public event Action OnMaxHealthChanged;
        public event Action OnDeath;
        public event Action OnRevive;

        public event Action<GameObject, Vector3, float> OnDamage;


#if UNITY_EDITOR
        private float _previousMaxHealth;
#endif


        public float SetHealth(float value)
        {
            var newHealth = Mathf.Clamp(value, 0, MaxHealth);
#if !UNITY_EDITOR
            if (Mathf.Approximately(newHealth, CurrentHealth)) return CurrentHealth;
#endif
            CurrentHealth = newHealth;
            OnHealthChanged?.Invoke();
            if (Mathf.Approximately(newHealth, 0)) Die();
            return newHealth;
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

        public virtual void ReceiveHealth(float health)
        {
            SetHealth(CurrentHealth + health);
        }

        public virtual void RestoreHealthToMax()
        {
            SetHealth(MaxHealth);
        }

        public bool CanTakeDamageThisFrame()
        {
            if (DamageAble) return true;
            if (IsDead) return false;
            return false;
        }

        public float Damage(float damage, GameObject instigator, Vector3 damageDirection,
            float afterInvincibilityDuration, DamageType damageType)
        {
            if (!CanTakeDamageThisFrame()) return 0;
            if (damageResistance != null)
                damage = damageResistance.CalculateDamage(this, damage, damageType, damageDirection);
            SetHealth(CurrentHealth - damage);
            OnDamage?.Invoke(instigator, damageDirection, damage);
            StartCoroutine(DamageEnable(afterInvincibilityDuration));
            return damage;
        }


        public void SetDamageAble(bool value)
        {
            DamageAble = value;
        }

        public virtual IEnumerator DamageEnable(float delay)
        {
            yield return new WaitForSeconds(delay);
            SetDamageAble(false);
        }


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