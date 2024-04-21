using System;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Variables
{
    [Serializable]
    public class Reactive<T>
    {
        [SerializeField] private T value;
        
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                OnValueChanged?.Invoke(value);
            }
        }

        public event Action<T> OnValueChanged;
    }
}