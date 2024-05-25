using _Root.Scripts.Datas.Runtime.Interfaces;
using Pancake;
using Pancake.Common;
using Pancake.PlayerLoop;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Attacks
{
    public abstract class AttackComponent : GameComponent, IDamageAble, IDamage, IRange, IDuration,IStart, ICancel
    {
        public Attack attack;
        [SerializeField] private DamageType damageType;
        [SerializeField] private float damage;
        [SerializeField] private float range;
        [SerializeField] private float duration;
        private DelayHandle _delayHandle;

        public DamageType DamageType
        {
            get => damageType;
            set => damageType = value;
        }

        public float Damage
        {
            get => damage;
            set => damage = value;
        }

        public float Range
        {
            get => range;
            set => range = value;
        }

        public float Duration
        {
            get => duration;
            set => duration = value;
        }

        public virtual DelayHandle Set(Attack attackSo)
        {
            attack = attackSo;
            damageType = attackSo.DamageType;
            damage = attackSo.Damage;
            range = attackSo.Range;
            duration = attackSo.Duration;
            return _delayHandle = App.Delay(this, duration, OnComplete, OnUpdate);
        }
        
        public abstract void OnStartup();
        protected abstract void OnUpdate(float deltaTime);
        protected abstract void OnComplete();
        protected abstract void OnCancel();

        public void Cancel()
        {
            _delayHandle.Cancel();
        }
    }
}