using Pancake;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public class Effect : GameComponent
    {
        public Optional<FrictionSideEffect> frictionSideEffect;
        public bool Friction(float friction, EffectSettings effectSettings)
        {
            frictionSideEffect.Value.Add(new FrictionSettings(friction, effectSettings));
            return frictionSideEffect.Enabled;
        }
    }
}