using System;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    [Serializable]
    public class EffectDuration : Effect
    {
        public float remainingDuration;
        public bool IsDurationOver => remainingDuration <= 0f;

        public EffectDuration(Effect effect)
        {
            power = effect.power;
            duration = effect.duration;
            isStackable = effect.isStackable;
            remainingDuration = duration;
        }
    }
}