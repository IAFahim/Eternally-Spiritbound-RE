using System;
using _Root.Scripts.Datas.Runtime.Movements;
using Pancake;
using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    [Serializable]
    public class FrictionEffectData
    {
        public Optional<FrictionEffectRunner> frictionSideEffect;
        
        [SerializeField] FrictionComponents frictionComponents;

        [SerializeField] public FrictionSettings frictionSettings;

        [SerializeField] EffectSettings effectSettings;

        public SpriteRenderer SpriteRenderer => frictionComponents.SpriteRenderer;

        public IFriction ComponentInterface
        {
            get => frictionComponents.ComponentInterface;
            set => frictionComponents.Set(value);
        }

        public bool Tick(float deltaTime) => effectSettings.Tick(deltaTime);

        public void Set(FrictionComponents components, FrictionSettings settings, EffectSettings effect)
        {
            frictionComponents = components;
            frictionSettings = settings;
            effectSettings = effect;
        }

        public void Set(FrictionSettings components, EffectSettings settings)
        {
            frictionSettings = components;
            effectSettings = settings;
        }

        [Button]
        public void Reuse() => effectSettings.Reuse();
    }

    [Serializable]
    public class FrictionSettings
    {
        [field: SerializeReference] public float Friction { get; private set; }
        public void Set(float friction) => Friction = friction;
    }

    [Serializable]
    public class FrictionComponents
    {
        public IFriction ComponentInterface { get; private set; }
        [field: SerializeReference] public SpriteRenderer SpriteRenderer { get; private set; }

        public void Set(IFriction componentInterface) => ComponentInterface = componentInterface;

        public void Set(IFriction componentInterface, SpriteRenderer spriteRenderer)
        {
            ComponentInterface = componentInterface;
            SpriteRenderer = spriteRenderer;
        }
    }
}