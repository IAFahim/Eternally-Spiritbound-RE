using _Root.Scripts.Datas.Runtime;
using _Root.Scripts.Datas.Runtime.Statistics;
using _Root.Scripts.Utilities;
using Pancake;
using Pancake.Apex;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Root.Scripts.Datas.Tests.Healths
{
    public class HealthTest : GameComponent
    {
        [FormerlySerializedAs("healthControl")] [FormerlySerializedAs("healthController")] [FormerlySerializedAs("health")] public HealthAuthoring healthAuthoring;

        [Range(-100, 100)] [SerializeField] private float heal = 10;
        [Range(-100, 100)] [SerializeField] private float damage = 10;
        [Range(-100, 100)] [SerializeField] private float invincibilityDuration;
        [SerializeReference] private DamageType damageType;
        public void OnEnable()
        {
            healthAuthoring.Health.OnValueChanged += OnHealthChanged;
        }
        

        private void OnHealthChanged(Vector2 obj)
        {
            Debug.Log($"Health changed: {obj.ToCurrentMax()}");
        }

        public void OnDisable()
        {
            healthAuthoring.Health.OnValueChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float obj)
        {
            Debug.Log($"Health changed: {obj}");
        }

        [Button]
        public void Heal()
        {
            var healed = healthAuthoring.Heal(heal, Transform.position, invincibilityDuration);
            Debug.Log($"Healed: {healed}");
        }

        [Button]
        public void Damage()
        {
            var damaged = healthAuthoring.Damage(damage, Transform.position, damageType, invincibilityDuration);
            Debug.Log($"Damaged: {damaged}");
        }
    }
}