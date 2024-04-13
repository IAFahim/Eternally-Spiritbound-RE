using System;
using _Root.Scripts.Datas.Runtime.Movements;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    [Serializable]
    public struct FrictionEffectData : ITick
    {
        [SerializeField] public FrictionPersistent persistent;
        [SerializeField] public FrictionSettings settings;
        public bool Tick(float deltaTime) => settings.Tick(deltaTime);

        public bool SetComponents(GameObject target)
        {
            var componentInterface = target.GetComponent<IFriction>();
            persistent.Set(componentInterface);
            return componentInterface != null;
        }
        
    }

    [Serializable]
    public class FrictionSettings : EffectData
    {
        [field:SerializeReference] public float Friction { get; private set; }
        
        public FrictionSettings(FrictionSettings settings) : base(settings.Duration)
        {
            Friction = settings.Friction;
        }
    }

    [Serializable]
    public class FrictionPersistent : PersistentEffectData
    {
        public IFriction frictionInterface;
        public SpriteRenderer SpriteRenderer;

        public void Set(IFriction componentInterface) => frictionInterface = componentInterface;
    }
}