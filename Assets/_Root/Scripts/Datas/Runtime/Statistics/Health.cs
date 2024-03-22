using System;
using System.Collections;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [Serializable]
    public class Health : GameComponent, IHealth, IDamage, IDeath
    {
        public const float DefaultStaring = 100;
        
        [Tooltip("the current health of the character")]
        public FloatVariable current;

        [Tooltip("If this is true, this object can't take damage at this time")]
        [field: SerializeReference]
        public bool DamageAble { get; private set; }
        

        public bool increaseHealthWithMaxHealth = true;


        [Tooltip("if this is true, the character is dead")]
        [field: SerializeReference]
        public bool IsDead { get; private set; }

        [SerializeField] private DamageResistanceBase damageResistance;
        public event Action OnDeath;
        public event Action OnRevive;

        public event Action<GameObject, Vector3, float> OnDamage;
        
        public FloatVariable Current => current;
        public float CurrentHealth => current;
        public float MaxHealth => current.Max;
        public event Action<float> OnMaxHealthChanged;


        public float SetHealth(float value)
        {
            Current.Value = value;
            return Current;
        }

        public void SetMaxHealth(float value)
        {
            Current.Max = value;
            OnMaxHealthChanged?.Invoke(value);
        }

        public virtual void ReceiveHealth(float health)
        {
            current.Value += health;
        }

        public virtual void RestoreHealthToMax()
        {
            current.Value = current.Max;
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
            SetHealth(current - damage);
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
    }
}