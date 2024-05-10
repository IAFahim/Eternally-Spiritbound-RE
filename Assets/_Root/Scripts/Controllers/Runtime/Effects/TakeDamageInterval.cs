using System;
using _Root.Scripts.Controllers.Runtime.Statuses;
using _Root.Scripts.Datas.Runtime;
using Pancake;
using Pancake.PlayerLoop;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Effects
{
    public class TakeDamageInterval : GameComponent, IUpdate
    {
        public DamageType damageType;
        public float damageInterval = 1f;
        public GameObject owner;
        public Collider2D collider2d;
        public bool followOwner;
        private float timer;
        public Vector2Constant worldScale;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == owner) return;
            if (other.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.Damage(1, new Vector3(0, 0, 0), damageType, 0);
            }
        }

        private void OnEnable()
        {
            timer = 0;
            App.AddListener(UpdateMode.Update, OnUpdate);
        }

        private void OnDisable()
        {
            App.RemoveListener(UpdateMode.Update, OnUpdate);
        }

        public void OnUpdate()
        {
            if (followOwner) Transform.position = owner.transform.position;
            timer += Time.deltaTime;
            if (timer >= damageInterval)
            {
                timer -= damageInterval;
                collider2d.enabled = !collider2d.enabled;
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