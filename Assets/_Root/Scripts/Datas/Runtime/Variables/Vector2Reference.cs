using System;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Variables
{
    [Serializable]
    public class Vector2ReferenceR: Vector2Reference
    {
        public event Action<Vector2> OnValueChanged;
        
        public new Vector2 Value
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