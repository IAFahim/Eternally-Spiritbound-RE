using _Root.Scripts.Datas.Runtime.Effects;
using _Root.Scripts.Datas.Runtime.SideEffects;
using Pancake;
using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Datas.Tests.Effects
{
    public class FrictionEffectApply : MonoBehaviour
    {
        public GameComponent target;
        public Effect frictionEffect;

        [Button]
        public void Apply()
        {
            var isSuccess = frictionEffect.Apply<FrictionSideEffect>(target);
            Debug.Log(isSuccess);
        }
    }
}