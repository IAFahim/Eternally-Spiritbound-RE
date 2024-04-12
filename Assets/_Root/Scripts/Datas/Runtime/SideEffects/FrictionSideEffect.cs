using _Root.Scripts.Datas.Runtime.Effects;
using _Root.Scripts.Datas.Runtime.Movements;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.SideEffects
{
    public class FrictionSideEffect : SideEffect
    {
        [Range(0, 10)] public float multiplier = 1;
        private IFriction _frictionInterface;
        public SpriteRenderer spriteRenderer;
        public Color spriteColor;
        public Color onFrictionColor = Color.yellow;

        public override void OnApply(Effect effect)
        {
            activeEffects.Add(new EffectDuration(effect));
            spriteRenderer.color = onFrictionColor;
        }

        public void OnRemove(int index)
        {
            activeEffects.RemoveAt(index);
            spriteRenderer.color = spriteColor;
        }

        private void Update()
        {
            for (int i = activeEffects.Count - 1; i >= 0; i--)
            {
                if (activeEffects[i].IsDurationOver)
                {
                    OnRemove(i);
                }
            }
        }

        private void OnValidate()
        {
            if (spriteRenderer) spriteColor = spriteRenderer.color;
        }
    }
}