using System;
using _Root.Scripts.Datas.Runtime.Statistics;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Variables
{
    [Serializable]
    public class ResistanceVariable : ScriptableVariable<ResistanceDatas>
    {
        public virtual float CalculateDamage(HealthAuthoring healthAuthoring, float damage, Vector3 damageDirection, DamageType damageType)
        {
            return damage;
        }
    }
}