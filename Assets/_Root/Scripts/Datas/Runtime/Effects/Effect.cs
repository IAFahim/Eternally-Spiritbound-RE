using System;
using _Root.Scripts.Datas.Runtime.SideEffects;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    [Serializable]
    public class Effect
    {
        [Range(0, 1)] public float power = 0.1f;
        [Range(0, 60 * 2)] public float duration = .5f;
        public bool isStackable;
        
        public bool Apply<T>(GameComponent component) where T : SideEffect
        {
            var sideEffect = component.GetComponent<T>();
            if (sideEffect)
            {
                sideEffect.OnApply(this);
                return true;
            }

            return false;
        }
    }
}