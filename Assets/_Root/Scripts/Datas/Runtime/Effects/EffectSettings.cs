using System;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    [Serializable]
    public class EffectSettings
    {
        [field: SerializeReference] public float Duration { get; private set; }
        public float remainingDuration;

        public void Reuse()
        {
            remainingDuration = Duration;
        }

        public bool Tick(float deltaTime) => (remainingDuration -= deltaTime) > 0f;
        public float Progress => 1f - remainingDuration / Duration;
    }
}