using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    [System.Serializable]
    public class EffectSetting
    {
        [field: SerializeReference] public float Duration { get; protected set; }
        [field: SerializeReference] public int StackLimit { get; protected set; }
        [field: SerializeReference] public int Cycle { get; protected set; }
        
        public float remainingDuration;
        public int currentCycle;
        
        public float TotalRemainingDuration => remainingDuration ;

        public EffectSetting()
        {
            Duration = 1;
            Cycle = 1;
            StackLimit = 1;
            remainingDuration = Duration;
            currentCycle = Cycle;
        }


        public EffectSetting(EffectSetting effectSetting)
        {
            Duration = effectSetting.Duration;
            StackLimit = effectSetting.StackLimit;
            Cycle = effectSetting.Cycle;

            remainingDuration = Duration;
            currentCycle = Cycle;
        }

        public bool TickOnUpdate(float deltaTime)
        {
            remainingDuration -= deltaTime;
            return remainingDuration > 0f;
        }

        public void ResetDuration()
        {
            remainingDuration = Duration;
        }

        public void Reset()
        {
            remainingDuration = Duration;
        }
    }
}