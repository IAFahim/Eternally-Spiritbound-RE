using System;
using System.Collections;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [Serializable]
    public class HealthBase : GameComponent
    {
        public const float DefaultStaring = 100;

        [Tooltip("the current health of the character")]
        [field: SerializeReference]
        public virtual float Current { get; private set; }


        [Tooltip("the max health of the character")]
        [field: SerializeReference]
        public virtual float Max { get; private set; } = DefaultStaring;

        public bool increaseHealthWithMaxHealth = true;

        [Tooltip("If this is true, this object can't take damage at this time")]
        [field: SerializeReference]
        public bool DamageAble { get; private set; }




        [Tooltip("if this is true, the character is dead")]
        [field: SerializeReference]
        public bool IsDead { get; private set; }


        [SerializeField] private DamageResistanceBase damageResistance;


        public virtual event Action<float> OnCurrentChanged;
        public event Action<GameObject, Vector3, float> OnDamage;
        public event Action<GameObject, Vector3, float> OnHealReceived;
        public virtual event Action<float> OnMaxChanged;
        public event Action OnDeath;
        public event Action OnRevive;


#if UNITY_EDITOR
        private float _previousMaxHealth;
#endif

        public float SetHealth(float value)
        {
            var newHealth = Mathf.Clamp(value, 0, Max);
#if !UNITY_EDITOR
            if (Mathf.Approximately(newHealth, CurrentHealth)) return CurrentHealth;
#endif
            Current = newHealth;
            OnCurrentChanged?.Invoke(newHealth);
            if (Mathf.Approximately(newHealth, 0)) Die();
            return newHealth;
        }

        public float SetMaxHealth(float value)
        {
            float newMax = Mathf.Max(value, 0);
#if !UNITY_EDITOR
            if (Mathf.Approximately(newMax, MaxHealth)) return MaxHealth;
#endif
            Max = newMax;
#if !UNITY_EDITOR
            if (increaseHealthWithMaxHealth) SetHealth(CurrentHealth + newMax - MaxHealth);
#else
            if (increaseHealthWithMaxHealth) SetHealth(Current + newMax - _previousMaxHealth);
            _previousMaxHealth = Max;
#endif
            OnMaxChanged?.Invoke(Max);
            return Max;
        }

        public virtual void ReceiveHealth(float health)
        {
            SetHealth(health);
        }

        public virtual void RestoreHealthToMax()
        {
            SetHealth(Max);
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
            SetHealth(Current - damage);
            OnDamage?.Invoke(instigator, damageDirection, damage);
            StartCoroutine(DamageEnable(afterInvincibilityDuration));
            return damage;
        }

        public float Heal(float damage, GameObject instigator, Vector3 damageDirection,
            float afterInvincibilityDuration)
        {
            throw new InvalidOperationException();
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
            SetHealth(Current);
            SetMaxHealth(Max);
        }
#endif
    }
}