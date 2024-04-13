using _Root.Scripts.Datas.Runtime.Movements;
using Pancake;
using Pancake.Apex;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public class Effect : GameComponent
    {
        public Optional<FrictionEffectRunner> frictionSideEffect;
        public FrictionEffectData frictionEffectData;

        private void OnEnable()
        {
            CheckCompatibility();
        }

        [Button]
        private void CheckCompatibility()
        {
            var componentInterface = GetComponent<IFriction>();
            frictionEffectData.ComponentInterface = componentInterface;
            frictionSideEffect = new Optional<FrictionEffectRunner>(componentInterface != null, frictionSideEffect);
        }

        public bool Friction(FrictionSettings frictionSettings, EffectSettings effectSettings)
        {
            if (frictionSideEffect.Enabled)
            {
                frictionEffectData.Set(frictionSettings, effectSettings);
                AddFriction();
                return true;
            }

            return false;
        }

        [Button]
        private void AddFriction() => frictionSideEffect.Value.Add(frictionEffectData);
    }
}