using System;
using Pancake.Scriptable;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    public interface IHealth
    {
        public FloatVariable Current { get; }

        public float CurrentHealth { get; }
        public float MaxHealth { get; }

        public float SetHealth(float value);
        public void SetMaxHealth(float value);
    }
}