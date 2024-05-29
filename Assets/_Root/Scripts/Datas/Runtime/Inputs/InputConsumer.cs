using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Inputs
{
    public abstract class InputConsumer : GameComponent, IInputConsumer
    {
        public abstract ManualInputProvider ManualInputProvider { get; }

        public void EnableManualInput()
        {
            ManualInputProvider.AddManualInputTo(GameObject);
        }

        public void DisableManualInput()
        {
            ManualInputProvider.RemoveManualInputFrom(GameObject);
        }
    }
}