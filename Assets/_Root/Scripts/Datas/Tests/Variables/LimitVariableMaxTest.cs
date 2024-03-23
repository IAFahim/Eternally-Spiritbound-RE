using _Root.Scripts.Datas.Runtime.Variables;
using Pancake;
using Pancake.Apex;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Tests.Variables
{
    public class LimitVariableMaxTest : GameComponent
    {
        [SerializeField] public FloatLimit floatLimit;

        [SerializeField] private FloatReference core;
        [SerializeField] private FloatReference max;
        [SerializeField] private FloatVariableLink variableLink;

        [Range(-101, 101)] public float amount;

        public void Awake()
        {
            if (variableLink != null)
            {
                floatLimit = new FloatLimit(core, variableLink);
                return;
            }

            floatLimit = new FloatLimit(core, max);
        }

        private void OnEnable()
        {
            floatLimit.OnValueChanged += OnValueChanged;
            floatLimit.OnMaxValueChanged += OnMaxValueChanged;
            floatLimit.OnAdd += OnAdd;
            floatLimit.OnExcess += OnExcess;
            floatLimit.OnInsufficient += OnInsufficient;
        }


        private void OnDisable()
        {
            floatLimit.OnValueChanged -= OnValueChanged;
            floatLimit.OnMaxValueChanged -= OnMaxValueChanged;
            floatLimit.OnAdd -= OnAdd;
            floatLimit.OnExcess -= OnExcess;
            floatLimit.OnInsufficient -= OnInsufficient;
        }


        private void OnMaxValueChanged(float obj)
        {
            Debug.Log($"Max value changed {obj}");
        }

        private void OnExcess(float obj)
        {
            Debug.Log($"Excess {obj}");
        }

        private void OnInsufficient(float obj)
        {
            Debug.Log($"Insufficient {obj}");
        }

        private void OnValueChanged(float obj)
        {
            Debug.Log($"Value changed {obj}");
        }

        private void OnAdd(float obj)
        {
            Debug.Log($"Added {obj}");
        }

        [Button]
        public void AddCore()
        {
            floatLimit.Value += amount;
        }

        [Button]
        public void AddMax()
        {
            floatLimit.Max -= amount;
        }
    }
}