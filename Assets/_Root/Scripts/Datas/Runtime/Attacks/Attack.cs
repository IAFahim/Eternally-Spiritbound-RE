﻿using Pancake;
using Pancake.Common;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Attacks
{
    public abstract class Attack : ScriptableObject, IAttack
    {
        [SerializeField] private AssetReferenceAttackComponent referenceAttackComponent;
        [SerializeField] private AttackComponent component;
        [SerializeField] private DamageType damageType;
        [SerializeField] private float damage;
        [SerializeField] private float range;
        [SerializeField] private float duration;
        [SerializeField] private Optional<Attack> nextAttack;
        public AssetReferenceAttackComponent ReferenceAttackComponent => referenceAttackComponent;
        public AttackComponent Component => component;
        public DamageType DamageType => damageType;
        public float Damage => damage;
        public float Range => range;
        public float Duration => duration;
        public Optional<Attack> NextAttack => nextAttack;
        public abstract DelayHandle Execute(Transform origin);
    }
}