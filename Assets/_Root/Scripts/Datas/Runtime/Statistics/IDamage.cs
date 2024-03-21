using System;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    public interface IDamage
    {
        public bool DamageAble { get; }
        public float Damage(float damage, GameObject instigator, Vector3 damageDirection,
            float afterInvincibilityDuration, DamageType damageType);
        public event Action<GameObject, Vector3, float> OnDamage;
    }
}