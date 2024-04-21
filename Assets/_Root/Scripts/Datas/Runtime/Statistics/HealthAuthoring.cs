using System;
using System.Collections;
using _Root.Scripts.Datas.Runtime.Variables;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [Serializable]
    public class HealthAuthoring : GameComponent, IOnlyOneVector2ReferenceR
    {
        public Vector2 dummyHealth;
        [SerializeField] private Vector2ReferenceR health;

        [Tooltip("If this is true, this object can't take damage at this time")]
        [field: SerializeReference]
        public bool DamageAble { get; private set; }

        [Tooltip("if this is true, the character is dead")]
        [field: SerializeReference]
        public bool IsDead { get; private set; }

        public Vector2ReferenceR Health
        {
            get => health;
            set => health = value;
        }

        [SerializeField] private ResistanceVariable resistanceVariable;

        public event Action OnDamage;
        public event Action OnHealReceived;
        public event Action OnDeath;
        public event Action OnRevive;


        private void OnEnable()
        {
            GetStatus();
        }

        public void GetStatus()
        {
            var stats = GetComponent<Stats>();
            if (stats != null)
            {
                Health = stats.health;
                resistanceVariable = stats.resistanceVariable;
            }
        }

        public void SetHealth(float newHealth, float maxHealth)
        {
            Health.Value = new Vector2(newHealth, maxHealth);
        }


        public void AddMaxHealth(float max)
        {
            var maxHealth = Mathf.Min(Health.Value.y + max, 1);
            var current = Mathf.Clamp(Health.Value.x + max, 1, maxHealth);
            Health.Value += new Vector2(current, max);
        }

        public virtual void RestoreHealthToMax()
        {
            Health.Value = new Vector2(Health.Value.y, Health.Value.y);
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
            // var damageDealt = Mathf.Min(current.Value, damage);
            var damageDealt = Mathf.Min(Health.Value.x, damage);
            return damageDealt;
        }

        public float Damage(float damage, Vector3 damageDirection, DamageType damageType,
            float invincibilityDuration = .1f)
        {
            var damageDealt = WillDamage(damage, damageDirection, damageType);
            Health.Value -= new Vector2(damageDealt, 0);
            OnDamage?.Invoke();
            if (Health.Value.x <= 0)
            {
                Die();
                return damageDealt;
            }

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
            return Mathf.Clamp(Health.Value.y - Health.Value.x, 0, heal);
        }

        public float Heal(float heal, Vector3 damageDirection, float afterInvincibilityDuration)
        {
            var healReceived = WillHeal(heal);
            Health.Value += new Vector2(healReceived, 0);
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
            DamageAble = false;
            IsDead = true;
            OnDeath?.Invoke();
        }

        public void Revive(float newHealth = 1)
        {
            IsDead = false;
            DamageAble = true;
            Health.Value = new Vector2(newHealth, Health.Value.y);
            OnRevive?.Invoke();
        }

        public Vector2ReferenceR ReferenceR => Health;
    }
}