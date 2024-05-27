using Pancake;

namespace _Root.Scripts.Datas.Runtime.Brains
{
    public class ActivateManualInputs : GameComponent
    {
        private void Awake()
        {
            var brains = GetComponents<Brain>();
            foreach (var brain in brains)
            {
                brain.ActivateManualInput();
            }
        }
    }
}