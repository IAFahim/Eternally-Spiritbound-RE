using System;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    public interface IDeath
    {
        public bool IsDead { get; }
        public event Action OnDeath;
        public event Action OnRevive;
        public void Die();
        public void Revive(float health = 1);
    }
}