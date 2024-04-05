using System;
using _Root.Scripts.Datas.Runtime;
using _Root.Scripts.Datas.Runtime.Statistics;
using Pancake;
using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Datas.Tests.Healths
{
    public class HealthTest : GameComponent
    {
        public Health health;

        [Range(-100, 100)] [SerializeField] private float heal = 10;
        [Range(-100, 100)] [SerializeField] private float damage = 10;
        [Range(-100, 100)] [SerializeField] private float invincibilityDuration;
        [SerializeReference] private DamageType damageType;

        public void OnEnable()
        {
            health.current.OnValueChanged += OnHealthChanged;
        }
        
        public void OnDisable()
        {
            health.current.OnValueChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float obj)
        {
            Debug.Log($"Health changed: {obj}");
        }

        [Button]
        public void Heal()
        {
            var healed = health.Heal(heal, Transform.position, invincibilityDuration);
            Debug.Log($"Healed: {healed}");
        }

        [Button]
        public void Damage()
        {
            var damaged = health.Damage(damage, Transform.position, invincibilityDuration, damageType);
            Debug.Log($"Damaged: {damaged}");
        }
    }
}