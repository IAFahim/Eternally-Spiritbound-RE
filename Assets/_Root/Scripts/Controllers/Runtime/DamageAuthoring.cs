using System.Collections;
using _Root.Scripts.Datas.Runtime;
using _Root.Scripts.Datas.Runtime.Statistics;
using Pancake;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Root.Scripts.Controllers.Runtime
{
    public class DamageAuthoring : GameComponent
    {
        [Range(0, 1000)] public float damage = 0.1f;
        public DamageType damageType = DamageType.Physical;
        public float invincibilityDuration = 0.1f;

        public float activeDuration = 0.1f;
        public float cooldownDuration = 0.5f;
        public Collider2D anyCollider2D;
        private Coroutine intervalCoroutine;
        
        private void OnValidate()
        {
            anyCollider2D = GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            intervalCoroutine = StartCoroutine(IntervalColliderToggle());
        }
        
        private void OnDisable()
        {
            StopCoroutine(intervalCoroutine);
        }

        private IEnumerator IntervalColliderToggle()
        {
            while (true)
            {
                anyCollider2D.enabled = true;
                yield return new WaitForSeconds(activeDuration);
                anyCollider2D.enabled = false;
                yield return new WaitForSeconds(cooldownDuration);
            }
        }
        
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out HealthAuthoring healthAuthoring))
            {
                healthAuthoring.Damage(damage, Transform.position, damageType, invincibilityDuration);
            }
        }
    }
}