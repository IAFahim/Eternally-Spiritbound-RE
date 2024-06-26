﻿using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public interface IEffectReference
    {
        public EffectReference Reference { get; }
        public bool AddReference(GameObject target);
    }
}