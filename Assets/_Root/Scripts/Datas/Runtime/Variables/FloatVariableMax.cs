using System;
using _Root.Scripts.Datas.Runtime.Interfaces;
using JetBrains.Annotations;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Variables
{
    [CreateAssetMenu(fileName = "MaxFloatAcc", menuName = "Scriptable/Variables/MaxFloatAcc", order = 1)]
    public class FloatVariableMax : FloatVariable, IMax
    {
        [SerializeField] private FloatVariable max;

        [CanBeNull]
        public new FloatVariable Max
        {
            get => max;
            set => max = value;
        }

        /// <summary>
        /// Triggered when a value is added. The parameters are a GameItem, which is the item that the value is being added to, and a float, which is the amount being added.
        /// </summary>
        public Action<float> onAdd;

        /// <summary>
        /// Triggered when a value is subtracted. The parameters are a GameItem, which is the item that the value is being subtracted from, and a float, which is the amount being subtracted.
        /// </summary>
        public Action<float> onSubtract;

        /// <summary>
        /// Triggered when there is an excess value. The parameters are a GameItem, which is the item that the value is being added to, and a float, which is the amount of the excess.
        /// </summary>
        public Action<float> onExcess;

        /// <summary>
        /// Triggered when there is an insufficient value. The parameters are a GameItem, which is the item that the value is being subtracted from, and a float, which is the amount of the insufficiency.
        /// </summary>
        public Action<float> onInsufficient;


        // 100 + 10 
        // 90 + 20
        // 20 + 20
        // 0 - 10
        public override float Value
        {
            get => base.Value;
            set
            {
                float amount = value - Value;
                float addable = CanAdd(amount);

                if (addable < amount) onExcess?.Invoke(amount - addable);
                else if (addable > amount) onInsufficient?.Invoke(addable - amount);

                if (addable > 0)
                {
                    onAdd?.Invoke(addable);
                    base.Value += addable;
                }
                else if (addable < 0)
                {
                    onSubtract?.Invoke(-addable);
                    base.Value += addable;
                }
            }
        }

        // #if UNITY_EDITOR
        // protected override void OnValidate()
        // {
        //     if (Max != null)
        //     {
        //         if (value > Max.Value) Value = Max.Value;
        //     }
        //     base.OnValidate();
        // }
        // #endif

        public float CanAdd(float amount)
        {
            // current + amount => Min(Max - Value, amount)
            // 100 + 10 => Min(100 - 100, 10) => 0
            // 90 + 20 => Min(100 - 90, 20) => 10
            if (Mathf.Approximately(amount, 0)) return 0;
            if (amount > 0)
            {
                if (Max == null) return Mathf.Min(base.Max - Value, amount);
                return Mathf.Min(Max - Value, amount);
            }
            else
            {
                // 90 - 10 => (0 - 90, -10) => - 10
                // 90 - 100 => (0 - 90, -100) => - 90
                // 100
                return Mathf.Max(Min - Value, amount);
            }
        }

        public void Add(float amount, out float added)
        {
            added = CanAdd(amount);
            base.Value += added;
        }

        public void ForceAdd(float amount)
        {
            base.Value += amount;
        }
    }
}