using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public class FrictionEffectRunner : EffectRunner<FrictionEffectData>
    {
        public Color effectTint = Color.yellow;
        public float maxStackCount = 10;

        protected override void OnApply(FrictionEffectData data)
        {
            var currentStack = data.persistent.stackCount++;
            data.persistent.frictionInterface.Friction += data.settings.Friction;
            data.persistent.SpriteRenderer.color = Color.Lerp(Color.white, effectTint, currentStack / maxStackCount);
        }

        protected override void OnRemove(FrictionEffectData data)
        {
            var currentStack = data.persistent.stackCount--;
            data.persistent.frictionInterface.Friction -= data.settings.Friction;
            data.persistent.SpriteRenderer.color = Color.Lerp(Color.white, effectTint, currentStack / maxStackCount);
        }
    }
}