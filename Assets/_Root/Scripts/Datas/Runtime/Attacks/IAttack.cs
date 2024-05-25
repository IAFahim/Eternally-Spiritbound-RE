using Pancake;
using Pancake.Common;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Attacks
{
    public interface IAttack: IDamageAble
    {
        public AttackComponent Component { get; }
        public Optional<Attack> NextAttack { get; }
        public DelayHandle Execute(Transform attackerTransform, Vector3 firePosition);
    }
}