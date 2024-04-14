using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public class FrictionEffectRunner : EffectRunner<FrictionEffectReference>
    {
        public Color effectTint = Color.yellow;
        public float maxStackCount = 10;

        protected override void OnStart(FrictionEffectReference frictionEffectReference)
        {
            frictionEffectReference.persistent.StackCount++;
            frictionEffectReference.persistent.SpriteRenderer.color = Color.red;
        }

        protected override void OnApply(FrictionEffectReference frictionEffectReference)
        {
            var currentStack = frictionEffectReference.persistent.StackCount;
            frictionEffectReference.persistent.frictionInterface.Friction += frictionEffectReference.persistent.Friction;
            frictionEffectReference.persistent.SpriteRenderer.color =
                Color.Lerp(Color.white, effectTint, currentStack / maxStackCount);
        }

        protected override void OnRemove(FrictionEffectReference reference)
        {
            var currentStack = reference.persistent.StackCount--;
            reference.persistent.frictionInterface.Friction -= reference.persistent.Friction;
            reference.persistent.SpriteRenderer.color = Color.Lerp(Color.white, effectTint, currentStack / maxStackCount);
        }
    }
}