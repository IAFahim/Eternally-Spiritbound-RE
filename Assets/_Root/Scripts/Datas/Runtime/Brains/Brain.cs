using _Root.Scripts.Datas.Runtime.Inputs;
using Pancake;

namespace _Root.Scripts.Datas.Runtime.Brains
{
    public class Brain : GameComponent
    {
        public ManualInput manualInput;

        public void ActivateManualInput()
        {
            manualInput.Add(gameObject);
        }
        
        public void DeactivateManualInput()
        {
            manualInput.Remove(gameObject);
        }
    }
}