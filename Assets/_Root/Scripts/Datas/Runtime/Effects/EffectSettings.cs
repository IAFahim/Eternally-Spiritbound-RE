using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    [System.Serializable]
    public class EffectSettings
    {
        [field: SerializeReference] public float StartDelay { get; set; }
        [field: SerializeReference] public float Duration { get; set; }
        [DisableInEditorMode] public float remainingStartDelay;
        [DisableInEditorMode] public float remainingDuration;

        [field: SerializeReference] public bool Stackable { get; set; }

        [field: SerializeReference] public bool EffectStarted { get; set; }
        public float TotalRemainingDuration => remainingStartDelay + remainingDuration;
        public float TotalDuration => StartDelay + Duration;


        public EffectSettings(EffectSettings effectSettings)
        {
            StartDelay = effectSettings.StartDelay;
            Duration = effectSettings.Duration;
            Stackable = effectSettings.Stackable;

            EffectStarted = false;
            remainingStartDelay = StartDelay;
            remainingDuration = Duration;
        }

        public bool TickOnUpdate(float deltaTime)
        {
            if (!EffectStarted) return false;
            remainingDuration -= deltaTime;
            return remainingDuration > 0f;
        }

        public float Progress => 1f - TotalRemainingDuration / TotalDuration;

        public bool TickBeforeStart(float deltaTime)
        {
            remainingStartDelay -= deltaTime;
            bool isRemaining = remainingStartDelay <= 0f;
            if (!EffectStarted && isRemaining)
            {
                EffectStarted = true;
            }

            return remainingStartDelay <= 0f;
        }

        public void Reset()
        {
            remainingStartDelay = StartDelay;
            remainingDuration = Duration;
            EffectStarted = false;
        }
    }
}