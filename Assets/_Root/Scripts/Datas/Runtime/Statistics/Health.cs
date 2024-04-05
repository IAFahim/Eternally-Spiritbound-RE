using System;
using System.Collections;
using _Root.Scripts.Datas.Runtime.Variables;
using Pancake;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [Serializable]
    public class Health : GameComponent
    {
        public FloatReferenceReactive current;
        public FloatReferenceReactive max;

        public bool increaseHealthWithMaxHealth = true;

        [Tooltip("If this is true, this object can't take damage at this time")]
        [field: SerializeReference]
        public bool DamageAble { get; private set; }

        [Tooltip("if this is true, the character is dead")]
        [field: SerializeReference]
        public bool IsDead { get; private set; }

        [FormerlySerializedAs("damageResistance")] [SerializeField]
        private ResistanceVariable resistanceVariable;

        public Vector3 lastDamageDirection;
        public DamageType lastDamageType;


        public event Action OnDamage;
        public event Action OnHealReceived;
        public event Action OnDeath;
        public event Action OnRevive;


        public void SetHealth(FloatReferenceReactive currentValue, FloatReferenceReactive maxValue)
        {
            current = currentValue;
            max = maxValue;
        }

        public void SetHealth(float currentValue, float maxValue)
        {
            current.Value = currentValue;
            max.Value = maxValue;
        }

        public void SetHealth(float currentValue)
        {
            current.Value = currentValue;
            if (increaseHealthWithMaxHealth)
            {
                max.Value = currentValue;
            }
        }

        public void IncreaseMax(float health)
        {
            if (increaseHealthWithMaxHealth) current.Value += health;
            max.Value += health;
        }

        public virtual void RestoreHealthToMax()
        {
            current.Value = max.Value;
        }

        public bool CanTakeDamageThisFrame()
        {
            if (DamageAble) return true;
            if (IsDead) return false;
            return false;
        }

        public float WillDamage(float damage, Vector3 damageDirection, DamageType damageType)
        {
            if (!CanTakeDamageThisFrame()) return 0;
            damage = resistanceVariable.CalculateDamage(this, damage, damageDirection, damageType);
            var damageDealt = Mathf.Min(current.Value, damage);
            return damageDealt;
        }

        public float Damage(float damage, Vector3 damageDirection, float invincibilityDuration, DamageType damageType)
        {
            var damageDealt = WillDamage(damage, damageDirection, damageType);
            current.Value -= damageDealt;
            OnDamage?.Invoke();
            if (invincibilityDuration > 0)
            {
                DamageAble = false;
                StartCoroutine(EnableDamageAfter(invincibilityDuration));
            }

            return damageDealt;
        }

        public float WillHeal(float heal)
        {
            if (IsDead) return 0;
            return Mathf.Clamp(max.Value - current.Value, 0, heal);
        }

        public float Heal(float heal, Vector3 damageDirection, float afterInvincibilityDuration)
        {
            var healReceived = WillHeal(heal);
            current.Value += healReceived;
            OnHealReceived?.Invoke();
            return healReceived;
        }

        public virtual IEnumerator EnableDamageAfter(float delay)
        {
            yield return new WaitForSeconds(delay);
            DamageAble = true;
        }

        public void Die()
        {
            IsDead = true;
            OnDeath?.Invoke();
        }

        public void Revive(float health = 1)
        {
            IsDead = false;
            current.Value = health;
            OnRevive?.Invoke();
        }
    }
}