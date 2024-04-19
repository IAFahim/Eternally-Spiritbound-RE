using _Root.Scripts.Datas.Runtime.Effects;
using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Datas.Tests.Effects
{
    public class FrictionEffectApply : MonoBehaviour
    {
        public Effect target;
        public EffectSetting effectSetting;
        public float friction;

        [Button]
        public void Apply()
        {
            var effect = target.GetComponent<Effect>();
            var allowed = effect.Friction(friction, effectSetting);
            Debug.Log($"Friction effect enabled: {allowed}");
        }
    }
}