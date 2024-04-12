using _Root.Scripts.Datas.Runtime.Effects;
using Pancake;
using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Datas.Tests.Effects
{
    public class FrictionEffectApply : MonoBehaviour
    {
        public GameComponent target;
        public float friction;
        public EffectSettings effectSettings;
        
        [Button]
        public void Apply()
        {
            var isSuccess = Effect.Friction(target, friction, effectSettings);
            Debug.Log(isSuccess);
        }
    }
}