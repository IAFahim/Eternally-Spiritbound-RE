using System;
using System.Collections;
using _Root.Scripts.Datas.Runtime.Variables;
using Pancake;
using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [Serializable]
    public class Health : GameComponent
    {
        public const float DefaultStaring = 100;
        public FloatLimit data;

        public bool increaseHealthWithMaxHealth = true;

        [Tooltip("If this is true, this object can't take damage at this time")]
        [field: SerializeReference]
        public bool DamageAble { get; private set; }

        [Tooltip("if this is true, the character is dead")]
        [field: SerializeReference]
        public bool IsDead { get; private set; }

        [SerializeField] private DamageResistanceBase damageResistance;

        public event Action<GameObject, Vector3, float> OnDamage;
        public event Action<GameObject, Vector3, float> OnHealReceived;
        public event Action OnDeath;
        public event Action OnRevive;


        public float Current
        {
            get => data.Value;
            set => data.Value = value;
        }

        public float Max
        {
            get => data.Max;
            set => data.Max = value;
        }

        public float CanAdd(float value) => data.CanAdd(value);

        public virtual void RestoreHealthToMax()
        {
            Current = Max;
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
            if (damageResistance) damage = damageResistance.CalculateDamage(this, damage, damageType, damageDirection);
            data.Add(-damage, out float damageDealt);
            OnDamage?.Invoke(instigator, damageDirection, damage);
            StartCoroutine(DamageEnable(afterInvincibilityDuration));
            return damageDealt;
        }

        public float Heal(float heal, GameObject instigator, Vector3 damageDirection,
            float afterInvincibilityDuration)
        {
            data.Add(heal, out float healReceived);
            return healReceived;
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
            Current = health;
            OnRevive?.Invoke();
        }
    }
}