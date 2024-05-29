using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Interfaces
{
    public interface IManualInputProvider
    {
        public void AddManualInputTo(GameObject self);
        public void RemoveManualInputFrom(GameObject self);
    }
}