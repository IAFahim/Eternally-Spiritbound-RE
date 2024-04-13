using System;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    [Serializable]
    public class FrictionSettings : EffectSettings
    {
        [Range(0, 10)] public float friction;
        public GameComponent target;

        public FrictionSettings(float friction, EffectSettings effectSettings) : base(effectSettings)
        {
            this.friction = friction;
        }
        
        public void Attach(GameComponent targetGameComponent)
        {
            target = targetGameComponent;
        }
    }
}