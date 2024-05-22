using System.Collections.Generic;
using _Root.Scripts.Controllers.Runtime.Statuses;
using _Root.Scripts.Datas.Runtime;
using Pancake;
using Pancake.Common;
using Pancake.PlayerLoop;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Effects
{
    public class TakeDamageInterval : GameComponent, IUpdate
    {
        public float damage = 1;
        public DamageType damageType;
        public float damageInterval = 1f;
        public GameObject owner;
        public Collider2D collider2d;
        public bool followOwner;
        private float timer;
        public Vector2Constant worldScale;
        public List<HealthComponent> healthComponents;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == owner) return;
            if (other.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.Damage(damage, Transform.position, damageType, 0);
                healthComponents.Add(healthComponent);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject == owner) return;
            if (other.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponents.Remove(healthComponent);
            }
        }

        private void OnEnable()
        {
            timer = 0;
            App.AddListener(EUpdateMode.Update, OnUpdate);
        }

        private void OnDisable()
        {
            App.RemoveListener(EUpdateMode.Update, OnUpdate);
        }

        public void OnUpdate()
        {
            if (followOwner) Transform.position = owner.transform.position;
            timer += Time.deltaTime;
            if (timer >= damageInterval)
            {
                timer -= damageInterval;
                foreach (var healthComponent in healthComponents)
                {
                    healthComponent.Damage(damage, transform.position, damageType, 0);
                }
            }
        }


        private void ConnectAndPrepComponent()
        {
            collider2d ??= GetComponent<Collider2D>();
            collider2d.isTrigger = true;
            
        }

        public void Reset()
        {
            ConnectAndPrepComponent();
        }
    }
}