using System;
using _Root.Scripts.Datas.Runtime.Effects;
using _Root.Scripts.Datas.Runtime.Movements;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Effects.Frictions
{
    [Serializable]
    public struct FrictionEffect : IEffectReference, IEffectParameter, IEffectSettings
    {
        [SerializeField] internal FrictionReference reference;
        [SerializeField] internal FrictionParameter parameter;
        [SerializeField] private EffectSetting setting;

        public bool AddReference(GameObject target)
        {
            var componentInterface = target.GetComponent<IFriction>();
            reference.Set(componentInterface);
            return componentInterface != null;
        }

        public void Apply(float friction, EffectSetting effectSetting)
        {
            parameter.friction = friction;
            Setting = effectSetting;
        }
        
        public void AddInstance(FrictionParameter effectParameter)
        {
            effectParameter.friction = effectParameter.friction;
        }

        public EffectReference Reference => reference;

        public EffectSetting Setting
        {
            get => setting;
            set => setting = new EffectSetting(value);
        }
    }

    [Serializable]
    public struct FrictionParameter : IEffectParameter
    {
        [Range(0, 10)] public float friction;
    }


    [Serializable]
    public class FrictionReference : EffectReference
    {
        public IFriction FrictionInterface;
        public SpriteRenderer spriteRenderer;

        public void Set(IFriction componentInterface) => FrictionInterface = componentInterface;
    }
}