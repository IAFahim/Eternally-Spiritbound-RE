using Alchemy.Inspector;
using Pancake;
using Pancake.Common;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Brains
{
    public class ActivateManualInputs : GameComponent
    {
        public Brain[] brains;

        private void Awake() => brains = GetComponents<Brain>();
        private void OnDisable() => Deactivate();

        public void OnEnable()
        {
            Activate();
        }

        [Button]
        private void Activate()
        {
            Debug.Log(enabled);
            foreach (var brain in brains) brain.ActivateManualInput();
        }

        [Button]
        private void Deactivate()
        {
            foreach (var brain in brains) brain.DeactivateManualInput();
        }

        [Button]
        public void SwitchControlTo(GameObject other)
        {
            enabled = false;
            other.GetOrAddComponent<ActivateManualInputs>().enabled = true;
        }
    }
}