using System;
using System.Collections;
using _Root.Scripts.Controllers.Runtime.Variables;
using _Root.Scripts.Datas.Runtime;
using _Root.Scripts.Datas.Runtime.Statistics;
using _Root.Scripts.Datas.Runtime.Variables;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Statuses
{
    [Serializable]
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private Reactive<Vector2> health;

        [Tooltip("If this is true, this object can't take damage at this time")]
        [field: SerializeReference]
        public bool DamageAble { get; private set; }

        [Tooltip("if this is true, the character is dead")]
        [field: SerializeReference]
        public bool IsDead { get; private set; }

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
                health = stats.Health;
            }
        }


        public void AddMaxHealth(float max)
        {
            var maxHealth = Mathf.Min(health.Value.y + max, 1);
            var current = Mathf.Clamp(health.Value.x + max, 1, maxHealth);
            health.Value += new Vector2(current, max);
        }

        public virtual void RestoreHealthToMax()
        {
            health.Value = new Vector2(health.Value.y, health.Value.y);
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
            var damageDealt = Mathf.Min(health.Value.x, damage);
            return damageDealt;
        }

        public float Damage(float damage, Vector3 direction, DamageType type, float invincibility = .1f)
        {
            var damageDealt = WillDamage(damage, direction, type);
            health.Value -= new Vector2(damageDealt, 0);
            OnDamage?.Invoke();
            if (health.Value.x <= 0)
            {
                Die();
                return damageDealt;
            }

            if (invincibility > 0)
            {
                DamageAble = false;
                StartCoroutine(EnableDamageAfter(invincibility));
            }

            return damageDealt;
        }

        public float WillHeal(float heal)
        {
            if (IsDead) return 0;
            return Mathf.Clamp(health.Value.y - health.Value.x, 0, heal);
        }

        public float Heal(float heal, Vector3 damageDirection, float afterInvincibilityDuration)
        {
            var healReceived = WillHeal(heal);
            health.Value += new Vector2(healReceived, 0);
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
            health.Value = new Vector2(newHealth, health.Value.y);
            OnRevive?.Invoke();
        }
    }
}