using System.Collections.Generic;
using _Root.Scripts.Datas.Runtime.Effects;
using Pancake;
using Pancake.Apex;

namespace _Root.Scripts.Datas.Runtime.SideEffects
{
    public abstract class SideEffect : GameComponent
    {
        [Array] public List<EffectDuration> activeEffects;
        public abstract void OnApply(Effect effect);
    }
}