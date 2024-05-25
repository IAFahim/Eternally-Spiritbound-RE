using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Attacks
{
    public interface IDamageAble
    {
        public DamageType DamageType { get; }
        public float Damage { get; }
        public float Range { get; }
        public float Duration { get; }
    }
}