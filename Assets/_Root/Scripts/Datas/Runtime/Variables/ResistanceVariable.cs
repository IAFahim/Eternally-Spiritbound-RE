using System;
using _Root.Scripts.Datas.Runtime.Statistics;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Variables
{
    [Serializable]
    public class ResistanceVariable : ScriptableVariable<ResistanceData>
    {
        public virtual float CalculateDamage(Health health, float damage, Vector3 damageDirection, DamageType damageType)
        {
            return damage;
        }
    }
}