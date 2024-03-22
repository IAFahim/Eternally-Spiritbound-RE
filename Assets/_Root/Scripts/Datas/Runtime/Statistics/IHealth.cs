namespace _Root.Scripts.Datas.Runtime.Statistics
{
    public interface IHealth
    {
        public float Current { get; }
        public float Max { get; }

        public float SetHealth(float value);
        public void SetMaxHealth(float value);
    }
}