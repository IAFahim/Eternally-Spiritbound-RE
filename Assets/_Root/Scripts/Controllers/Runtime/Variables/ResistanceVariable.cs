using System;
using _Root.Scripts.Controllers.Runtime.Statuses;
using _Root.Scripts.Datas.Runtime;
using _Root.Scripts.Datas.Runtime.Statistics;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Variables
{
    [Serializable]
    public class ResistanceVariable : ScriptableVariable<ResistanceDatas>
    {
        public virtual float CalculateDamage(HealthComponent healthComponent, float damage, Vector3 damageDirection, DamageType damageType)
        {
            return damage;
        }
    }
}