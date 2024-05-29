using _Root.Scripts.Datas.Runtime.Inputs;

namespace _Root.Scripts.Datas.Runtime.Interfaces
{
    public interface ILogicInputProvider
    {
        public IManualInputProvider ManualInputProvider { get; }
    }
}