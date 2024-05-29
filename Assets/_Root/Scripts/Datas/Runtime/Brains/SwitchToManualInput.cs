using _Root.Scripts.Datas.Runtime.Inputs;
using Pancake;

namespace _Root.Scripts.Datas.Runtime.Brains
{
    public class SwitchToManualInput : GameComponent
    {
        private void OnEnable()
        {
            IInputConsumer[] inputConsumers = GetComponents<IInputConsumer>();
            foreach (IInputConsumer inputConsumer in inputConsumers)
            {
                inputConsumer.EnableManualInput();
            }
        }
        
        private void OnDisable()
        {
            IInputConsumer[] inputConsumers = GetComponents<IInputConsumer>();
            foreach (IInputConsumer inputConsumer in inputConsumers)
            {
                inputConsumer.DisableManualInput();
            }
        }
    }
}