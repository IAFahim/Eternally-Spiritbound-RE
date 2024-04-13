using _Root.Scripts.Datas.Runtime.Movements;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public class FrictionEffectRunner : EffectRunner<FrictionEffectData>
    {
        
        public Color effectTint = Color.yellow;

        protected override void OnApply(FrictionEffectData data)
        {
            data.Reuse();
            data.ComponentInterface.Friction += data.frictionSettings.Friction;
            data.SpriteRenderer.color = effectTint;
        }

        protected override void OnRemove(FrictionEffectData data)
        {
            data.SpriteRenderer.color = Color.white;
            data.ComponentInterface.Friction -= data.frictionSettings.Friction;
        }

        protected override void Update()
        {
            if (updateEnabled == false) return;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                FrictionEffectData frictionEffectData = list[i];
                if (!frictionEffectData.Tick(Time.deltaTime))
                {
                    Remove(frictionEffectData);
                }
            }
        }
    }
}