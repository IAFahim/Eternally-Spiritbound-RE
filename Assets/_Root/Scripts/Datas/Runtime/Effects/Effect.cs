using Pancake;
using Pancake.Apex;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public class Effect : GameComponent
    {
        public Optional<EffectRunner<FrictionEffectReference>> frictionEffectRunner;
        public FrictionEffectReference frictionEffectReference;

        private void OnEnable()
        {
            SetEffectRunners();
        }

        [Button]
        private void SetEffectRunners()
        {
            frictionEffectRunner = new Optional<EffectRunner<FrictionEffectReference>>
                (frictionEffectReference.SetComponents(GameObject), frictionEffectRunner);
        }

        public bool Friction(EffectSettings effectSettings)
        {
            if (frictionEffectRunner.Enabled)
            {
                frictionEffectReference.settings = new EffectSettings(effectSettings);
                ApplyFriction();
                return true;
            }

            return false;
        }

        [Button]
        private void ApplyFriction() => frictionEffectRunner.Value.Add(frictionEffectReference);
    }
}