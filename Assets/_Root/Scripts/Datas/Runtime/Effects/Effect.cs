using Pancake;
using Pancake.Apex;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public class Effect : GameComponent
    {
        public Optional<EffectRunner<FrictionEffectData>> frictionEffectRunner;
        public FrictionEffectData frictionEffectData;

        private void OnEnable()
        {
            SetEffectRunners();
        }

        [Button]
        private void SetEffectRunners()
        {
            frictionEffectRunner = new Optional<EffectRunner<FrictionEffectData>>
                (frictionEffectData.SetComponents(GameObject), frictionEffectRunner);
        }

        public bool Friction(FrictionSettings frictionSettings)
        {
            if (frictionEffectRunner.Enabled)
            {
                frictionEffectData.settings = new FrictionSettings(frictionSettings);
                ApplyFriction();
                return true;
            }

            return false;
        }

        [Button]
        private void ApplyFriction() => frictionEffectRunner.Value.Add(frictionEffectData);
    }
}