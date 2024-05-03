using System;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Variables
{
    [Serializable]
    public class Reactive<T>
    {
        [SerializeField] private T value;
        public event Action<T> OnValueChanged;
        

        public T Value
        {
            get => value;
            set
            {
                if (Equals(this.value, value)) return;
#if UNITY_EDITOR
                previousValue = this.value;
#endif
                this.value = value;
                OnValueChanged?.Invoke(value);
            }
        }
        
        public static implicit operator T(Reactive<T> reactive) => reactive.Value;

#if UNITY_EDITOR
        private T previousValue;

        public void ForceInvokeOnValueChange()
        {
            if (!Equals(Value, previousValue))
            {
                OnValueChanged?.Invoke(Value);
            }
        }
#endif
    }
}