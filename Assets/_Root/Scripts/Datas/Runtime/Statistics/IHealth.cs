namespace _Root.Scripts.Datas.Runtime.Statistics
{
    public interface IHealth
    {
        public float Current { get; set; }
        public float Max { get; set; }

        public float SetHealth(float value);
        public void SetMaxHealth(float value);
    }
}