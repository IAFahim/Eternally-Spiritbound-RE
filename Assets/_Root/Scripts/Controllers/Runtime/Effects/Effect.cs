using _Root.Scripts.Controllers.Runtime.Effects.Frictions;
using _Root.Scripts.Datas.Runtime.Effects;
using Alchemy.Inspector;
using Pancake;

namespace _Root.Scripts.Controllers.Runtime.Effects
{
    public class Effect : GameComponent
    {
        public Optional<EffectRunner<FrictionEffect>> frictionEffectRunner;
        public FrictionEffect frictionEffect;

        private void OnEnable()
        {
            SetEffectRunners();
        }

        [Button]
        private void SetEffectRunners()
        {
            frictionEffectRunner = new Optional<EffectRunner<FrictionEffect>>
                (frictionEffect.AddReference(GameObject), frictionEffectRunner);
        }

        public bool Friction(float friction, EffectSetting effectSetting)
        {
            if (!frictionEffectRunner.Enabled) return false;
            frictionEffect.Apply(friction, effectSetting);
            frictionEffectRunner.Value.Add(frictionEffect);
            return true;
        }
    }
}