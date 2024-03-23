using System;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Variables
{
    [Serializable]
    public class FloatLimit
    {
        [SerializeField] private FloatReference current;
        [SerializeField] private FloatReference max;

        public FloatLimit(FloatReference current)
        {
            this.current = current;
        }

        public FloatLimit(FloatReference current, FloatReference max) : this(current)
        {
            this.max = max;
        }

        public FloatLimit(FloatReference current, FloatVariableLink variableLink) : this(current)
        {
            if (!variableLink.TryGetValue(current, out max))
            {
                max = new FloatReference
                    { useLocal = true, localValue = current.useLocal ? float.MaxValue : current.variable.Max };
            }
        }


        private Action<float> onValueChanged;
        private Action<float> onMaxValueChanged;

        public event Action<float> OnValueChanged
        {
            add
            {
                onValueChanged += value;
                if (!current.useLocal) current.variable.OnValueChanged += value;
            }
            remove
            {
                onValueChanged -= value;
                if (!current.useLocal) current.variable.OnValueChanged -= value;
            }
        }

        public event Action<float> OnMaxValueChanged
        {
            add
            {
                onMaxValueChanged += value;
                if (!max.useLocal) max.variable.OnValueChanged += value;
            }
            remove
            {
                onMaxValueChanged -= value;
                if (!max.variable) max.variable.OnValueChanged -= value;
            }
        }

        public event Action<float> OnAdd;
        public event Action<float> OnSubtract;
        public event Action<float> OnExcess;
        public event Action<float> OnInsufficient;

        public float Value
        {
            get => current.Value;
            set
            {
                float amount = value - current.Value;
                float addable = CanAdd(amount);

                if (addable < amount) OnExcess?.Invoke(amount - addable);
                else if (addable > amount) OnInsufficient?.Invoke(addable - amount);

                if (addable == 0) return;

                if (addable > 0)
                {
                    OnAdd?.Invoke(addable);
                    current.Value += addable;
                }
                else if (addable < 0)
                {
                    OnSubtract?.Invoke(-addable);
                    current.Value += addable;
                }

                onValueChanged?.Invoke(current.Value);
            }
        }

        public float Max
        {
            get => max.Value;
            set
            {
                max.Value = value;
                onValueChanged?.Invoke(value);
            }
        }

        public float CanAdd(float amount)
        {
            if (Mathf.Approximately(amount, 0)) return 0;
            if (amount > 0)
            {
                var currentMax = max.Value;
                return Mathf.Min(currentMax - current.Value, amount);
            }

            if (current.useLocal) return Mathf.Max(0 - current.Value, amount);
            return Mathf.Max(current.variable.Min - current.Value, amount);
        }

        public void Add(float amount, out float added)
        {
            added = CanAdd(amount);
            current.Value += added;
        }

        public void ForceAdd(float amount)
        {
            current.Value += amount;
        }
    }
}