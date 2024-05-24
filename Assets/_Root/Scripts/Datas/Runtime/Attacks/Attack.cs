using _Root.Scripts.Datas.Runtime.Interfaces;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Attacks
{
    public class Attack : ScriptableObject, IAttack
    {
        [SerializeField] private DamageType damageType;
        [SerializeField] private float damage;
        [SerializeField] private float range;
        [SerializeField] private float duration;

        public DamageType DamageType => damageType;
        public float Damage => damage;
        public float Range => range;
        public float Duration => duration;

        public void Perform()
        {
            
        }
    }
}