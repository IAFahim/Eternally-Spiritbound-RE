namespace _Root.Scripts.Datas.Runtime.Inputs
{
    public interface IInputConsumer
    {
        public ManualInputProvider ManualInputProvider { get; }
        public void EnableManualInput();
        public void DisableManualInput();
    }
}