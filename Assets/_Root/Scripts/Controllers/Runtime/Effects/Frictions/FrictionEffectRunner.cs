using _Root.Scripts.Datas.Runtime.Effects;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Effects.Frictions
{
    public class FrictionEffectRunner : EffectRunner<FrictionEffect>
    {
        public Color effectTint = Color.yellow;
        public float maxFriction = 10;

        protected override void OnAdd(FrictionEffect effect)
        {
            var currentFriction = effect.reference.FrictionInterface.Friction += effect.parameter.friction;
            effect.reference.spriteRenderer.color = Color.Lerp(Color.white,effectTint, (currentFriction - 1) / maxFriction);
        }

        protected override void OnCycle(FrictionEffect effect)
        {
        }

        protected override void OnRemove(FrictionEffect effect)
        {
            var currentFriction = effect.reference.FrictionInterface.Friction -= effect.parameter.friction;
            effect.reference.spriteRenderer.color = Color.Lerp(Color.white,effectTint, (currentFriction - 1) / maxFriction);
        }
    }
}