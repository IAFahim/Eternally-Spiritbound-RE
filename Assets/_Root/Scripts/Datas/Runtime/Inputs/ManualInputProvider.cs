using _Root.Scripts.Datas.Runtime.Interfaces;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Inputs
{
    public abstract class ManualInputProvider : ScriptableObject, IManualInputProvider
    {
        public abstract void AddManualInputTo(GameObject self);

        public abstract void RemoveManualInputFrom(GameObject self);
    }
}