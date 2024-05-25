using _Root.Scripts.Datas.Runtime.Interfaces;
using Pancake;
using Pancake.Common;
using Pancake.PlayerLoop;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Attacks
{
    public abstract class AttackComponent : GameComponent, IOriginPosition, IDamageAble, IDamage, IRange, IDuration,
        IStart, ICancel
    {
        [SerializeField] private Transform attackerTransform;
        [SerializeField] private Vector3 firePosition;
        public Attack attackSo;
        [SerializeField] private DamageType damageType;
        [SerializeField] private float damage;
        [SerializeField] private float range;
        [SerializeField] private float duration;
        private DelayHandle _delayHandle;

        public Vector3 OriginPosition
        {
            get => firePosition;
            set => firePosition = value;
        }

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

        public virtual DelayHandle Initialize(Attack attack, Transform attacker, Vector3 originPosition)
        {
            attackSo = attack;
            attackerTransform = attacker;
            firePosition = originPosition;
            damageType = attack.DamageType;
            damage = attack.Damage;
            range = attack.Range;
            duration = attack.Duration;
            OnStartup();
            return _delayHandle = App.Delay(this, duration, OnComplete, OnUpdate);
        }

        public abstract void OnStartup();
        protected abstract void OnUpdate(float deltaTime);
        protected abstract void OnComplete();
        protected abstract void OnCancel();

        public void Cancel()
        {
            _delayHandle.Cancel();
            OnCancel();
        }
    }
}