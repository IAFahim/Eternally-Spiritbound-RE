using System;
using _Root.Scripts.Datas.Runtime.Statistics;
using _Root.Scripts.Datas.Runtime.Variables;
using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Datas.Tests
{
    public class ResistanceChangeTest : MonoBehaviour
    {
        public ResistanceVariable resistanceVariable;
        public float value = .5f;

        private void OnEnable()
        {
            resistanceVariable.OnValueChanged += OnValueChanged;
        }

        private void OnValueChanged(ResistanceDatas obj)
        {
            Debug.Log($"Resistance changed to {obj}");
        }
        
        private void OnDisable()
        {
            resistanceVariable.OnValueChanged -= OnValueChanged;
        }

        [Button]
        public void ChangeResistance()
        {
            resistanceVariable.Value = new ResistanceDatas
            {
                physicalResistance = value,
                fireResistance = value,
                waterResistance = value,
                earthResistance = value,
                airResistance = value,
                darkResistance = value
            };
        }
    }
}