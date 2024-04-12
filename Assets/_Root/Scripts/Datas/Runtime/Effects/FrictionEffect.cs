using System;
using System.Collections.Generic;
using _Root.Scripts.Datas.Runtime.Movements;
using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    [Serializable]
    public class FrictionSettings : EffectSettings
    {
        [Range(0, 10)] public float friction;

        public FrictionSettings(float friction, EffectSettings effectSettings) : base(effectSettings)
        {
            this.friction = friction;
        }
    }


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
                activeEffects.Add(frictionSettings);
                _frictionInterface.Friction += frictionSettings.friction;
                spriteRenderer.color = onFrictionColor;
                return true;
            }

            return false;
        }


        public void OnRemove(FrictionSettings frictionSettings)
        {
            _frictionInterface.Friction -= frictionSettings.friction;
            spriteRenderer.color = spriteColor;
        }

        private void Update()
        {
            for (int i = activeEffects.Count - 1; i >= 0; i--)
            {
                FrictionSettings frictionSettings = activeEffects[i];
                if (!frictionSettings.Tick(Time.deltaTime))
                {
                    activeEffects.RemoveAt(i);
                    OnRemove(frictionSettings);
                }
            }
        }

        private void OnValidate()
        {
            if (spriteRenderer) spriteColor = spriteRenderer.color;
        }
    }
}