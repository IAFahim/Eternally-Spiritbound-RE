using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [CreateAssetMenu(menuName = "Scriptable/Statistics/DamageResistanceBase")]
    public class DamageResistanceBase : FloatVariable
    {
        [field: SerializeReference] public virtual float PhysicalResistance { get; set; }
        [field: SerializeReference] public virtual float FireResistance { get; set; }
        [field: SerializeReference] public virtual float WaterResistance { get; set; }
        [field: SerializeReference] public virtual float EarthResistance { get; set; }
        [field: SerializeReference] public virtual float AirResistance { get; set; }
        [field: SerializeReference] public virtual float DarkResistance { get; set; }
        
        public virtual float CalculateDamage(Health health, float damage, DamageType damageType, Vector3 damageDirection)
        {
            return damage;
        }
    }
}