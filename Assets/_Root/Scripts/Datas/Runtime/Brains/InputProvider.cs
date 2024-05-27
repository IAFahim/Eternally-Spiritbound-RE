using _Root.Scripts.Datas.Runtime.Inputs;
using Alchemy.Inspector;
using Pancake;
using Pancake.Common;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Brains
{
    public class InputProvider : GameComponent
    {
        public ManualInput[] manualInputs;
        private void OnEnable() => Activate();
        private void OnDisable() => Deactivate();

        private void Activate()
        {
            foreach (var manualInput in manualInputs)
            {
                manualInput.Add(gameObject);
            }
        }

        private void Deactivate()
        {
            foreach (var manualInput in manualInputs)
            {
                manualInput.Remove(gameObject);
            }
        }
        
        [Button]
        public void PassInputTo(GameObject other)
        {
            enabled = false;
            other.GetOrAddComponent<InputProvider>().enabled = true;
        }
    }
}