using _Root.Scripts.Datas.Runtime.Variables;
using Pancake;
using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Datas.Tests.Variables
{
    public class FloatVariableMaxTest : GameComponent
    {
        public FloatVariableMax floatVariableMax;

        [Range(-100, 100)] public float amount = 10;

        [Button]
        public void AddDirect()
        {
            //100 + 10
            // addable = 10
            // previous = 100
            float previous = floatVariableMax.Value;
            // 100.Max(100) + 10 = 100
            floatVariableMax.Value += amount;
            // previous(100) + addable(10) - Current(100) = 10
            float remaining = previous + amount - floatVariableMax.Value;

            Debug.Log($"Added {amount - remaining}");
        }

        [Button]
        public void AddMethodCall()
        {
            floatVariableMax.Add(amount, out float added);
            Debug.Log($"Added {added}");
        }
    }
}