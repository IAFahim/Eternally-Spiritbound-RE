﻿using _Root.Scripts.Datas.Runtime.Effects.Frictions;
using Pancake;
using Pancake.Apex;

namespace _Root.Scripts.Datas.Runtime.Effects
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