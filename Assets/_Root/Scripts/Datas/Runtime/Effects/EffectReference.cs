using System;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    [Serializable]
    public class EffectReference
    {
        [field: SerializeReference] public int StackCount { get; set; }
        public bool AllowMultiple(int limit) => StackCount < limit;
    }
}