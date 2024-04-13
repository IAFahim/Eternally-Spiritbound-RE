
using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    [System.Serializable]
    public class EffectData
    {
        [field: SerializeReference] public float Duration { get; private set;}
        [DisableInEditorMode] public float remainingDuration;
        
        public EffectData(float duration)
        {
            Duration = duration;
            remainingDuration = duration;
        }

        public bool Tick(float deltaTime)
        {
            remainingDuration -= deltaTime;
            return remainingDuration > 0f;
        }

        public float Progress => 1f - remainingDuration / Duration;
    }
}