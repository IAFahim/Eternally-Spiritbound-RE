using System;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    public interface IHealth
    {
        public float CurrentHealth { get; }
        public float MaxHealth { get; }
        public event Action OnHealthChanged;
        public event Action OnMaxHealthChanged;
        public float SetHealth(float value);
        public float SetMaxHealth(float value);
    }
}