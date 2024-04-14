using System;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    [Serializable]
    public class EffectReference
    {
        [field: SerializeReference] public int StackCount { get; set; }
        [field: SerializeReference] public float EffectDeActiveDuration { get; set; }

        public bool AllowMultiple(bool stackable) => stackable || StackCount == 0;
    }
}