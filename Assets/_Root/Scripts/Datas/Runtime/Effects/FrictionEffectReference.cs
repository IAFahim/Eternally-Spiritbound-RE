using System;
using _Root.Scripts.Datas.Runtime.Movements;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    [Serializable]
    public struct FrictionEffectReference : IEffectSettings, IEffectReference
    {
        [SerializeField] public FrictionReference persistent;
        [SerializeField] public EffectSettings settings;

        public bool SetComponents(GameObject target)
        {
            var componentInterface = target.GetComponent<IFriction>();
            persistent.Set(componentInterface);
            return componentInterface != null;
        }

        public EffectSettings Settings
        {
            get => settings;
            set => settings = value;
        }

        public EffectReference Reference
        {
            get => persistent;
            set => persistent = (FrictionReference)value;
        }
    }


    [Serializable]
    public class FrictionReference : EffectReference
    {
        [field: SerializeReference] public float Friction { get; private set; }
        public IFriction frictionInterface;
        public SpriteRenderer SpriteRenderer;

        public void Set(IFriction componentInterface) => frictionInterface = componentInterface;
    }
}