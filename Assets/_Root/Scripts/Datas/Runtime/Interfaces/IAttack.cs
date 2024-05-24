namespace _Root.Scripts.Datas.Runtime.Interfaces
{
    public interface IAttack
    {
        public DamageType DamageType { get; }
        public float Damage { get; }
        public float Range { get; }
        public float Duration { get; }

        public void Perform();
    }
}