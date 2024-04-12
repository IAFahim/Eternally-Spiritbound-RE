using System;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    [Serializable]
    public class EffectSettings
    {
        [Range(0, 60 * 2)] public float duration;
        public float remainingDuration;

        protected EffectSettings(EffectSettings effectSettings)
        {
            duration = effectSettings.duration;
            remainingDuration = duration;
        }

        public bool Tick(float deltaTime) => (remainingDuration -= deltaTime) > 0f;
    }
}