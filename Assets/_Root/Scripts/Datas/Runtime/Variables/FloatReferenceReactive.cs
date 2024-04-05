using System;
using Pancake.Scriptable;

namespace _Root.Scripts.Datas.Runtime.Variables
{
    [Serializable]
    public class FloatReferenceReactive : FloatReference
    {
        public event Action<float> OnValueChanged;

        public new float Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                OnValueChanged?.Invoke(value);
            }
        }
    }
}