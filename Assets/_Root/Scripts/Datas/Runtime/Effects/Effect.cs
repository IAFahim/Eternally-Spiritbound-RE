using System.Collections.Generic;
using Pancake;
using Pancake.Apex;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public class Effect : GameComponent
    {
        
        public static bool Friction(GameComponent target, float friction, EffectSettings effectSettings)
        {
            return target.TryGetComponent<FrictionEffect>(out var frictionEffect) && frictionEffect.Apply(new FrictionSettings(friction, effectSettings));
        }
    }
}