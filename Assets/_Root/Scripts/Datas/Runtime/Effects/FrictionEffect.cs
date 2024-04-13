using System.Collections.Generic;
using _Root.Scripts.Datas.Runtime.Movements;
using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public class FrictionEffect : Effect
    {
        [Array] public List<FrictionSettings> activeEffects;
        private IFriction _frictionInterface;
        public SpriteRenderer spriteRenderer;
        public Color spriteColor;
        public Color onFrictionColor = Color.yellow;
        

        public bool Apply(FrictionSettings frictionSettings)
        {
            _frictionInterface ??= GetComponent<IFriction>();
            if (_frictionInterface != null)
            {
                frictionSideEffect.Value.Add(frictionSettings);
                _frictionInterface.Friction += frictionSettings.friction;
                spriteRenderer.color = onFrictionColor;
                return true;
            }

            return false;
        }


        public void Remove(FrictionSettings frictionSettings)
        {
            _frictionInterface.Friction -= frictionSettings.friction;
            spriteRenderer.color = spriteColor;
            frictionSideEffect.Value.Remove(frictionSettings);
        }
    }
}