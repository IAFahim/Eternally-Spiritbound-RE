using System;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Attacks
{
    [Serializable]
    public class AttackData
    {
        [SerializeField] private DamageType damageType;
        [SerializeField] private float damage;
        [SerializeField] private float range;
        [SerializeField] private float duration;
    }
}