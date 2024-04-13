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
            var effect = target.GetComponent<FrictionEffect>();
            var allowed = effect.Friction(friction, effectSettings);
            Debug.Log($"Friction effect enabled: {allowed}");
        }
    }
}