using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects.Frictions
{
    public class FrictionEffectRunner : EffectRunner<FrictionEffect>
    {
        public Color effectTint = Color.yellow;
        public float maxStackCount = 10;

        protected override void OnStart(FrictionEffect frictionEffect)
        {
            frictionEffect.reference.StackCount++;
            frictionEffect.reference.spriteRenderer.color = Color.red;
        }

        protected override void OnApply(FrictionEffect frictionEffect)
        {
            var currentStack = frictionEffect.reference.StackCount;
            frictionEffect.reference.FrictionInterface.Friction += frictionEffect.parameter.friction;
            frictionEffect.reference.spriteRenderer.color =
                Color.Lerp(Color.white, effectTint, currentStack / maxStackCount);
        }

        protected override void OnRemove(FrictionEffect reference)
        {
            var currentStack = reference.reference.StackCount--;
            reference.reference.FrictionInterface.Friction -= reference.parameter.friction;
            reference.reference.spriteRenderer.color = Color.Lerp(Color.white, effectTint, currentStack / maxStackCount);
        }
    }
}