using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [CreateAssetMenu(menuName = "Scriptable/Statistics/DamageResistanceVariable")]
    public class DamageResistanceVariable : DamageResistanceBase
    {
        public FloatVariable physicalResistance;
        public FloatVariable fireResistance;
        public FloatVariable waterResistance;
        public FloatVariable earthResistance;
        public FloatVariable airResistance;
        public FloatVariable darkResistance;

        public override float PhysicalResistance
        {
            get => physicalResistance.Value;
            set => physicalResistance.Value = value;
        }

        public override float FireResistance
        {
            get => fireResistance.Value;
            set => fireResistance.Value = value;
        }

        public override float WaterResistance
        {
            get => waterResistance.Value;
            set => waterResistance.Value = value;
        }

        public override float EarthResistance
        {
            get => earthResistance.Value;
            set => earthResistance.Value = value;
        }

        public override float AirResistance
        {
            get => airResistance.Value;
            set => airResistance.Value = value;
        }

        public override float DarkResistance
        {
            get => darkResistance.Value;
            set => darkResistance.Value = value;
        }

        public override float CalculateDamage(HealthBase healthBase, float damage, DamageType damageType,
            Vector3 damageDirection)
        {
            return damage;
        }
    }
}