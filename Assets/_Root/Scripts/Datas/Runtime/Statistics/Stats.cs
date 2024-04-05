using _Root.Scripts.Datas.Runtime.Variables;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [CreateAssetMenu(menuName = "Stats", fileName = "ScriptAbles/Stats", order = 0)]
    public class Stats : ScriptableObject
    {
        public FloatReferenceReactive currentHealth = new()
        {
            useLocal = true,
            localValue = 1,
        };

        public FloatReferenceReactive maxHealth = new()
        {
            useLocal = true,
            localValue = 1,
        };

        public void OverrideHealth(out FloatReferenceReactive current, out FloatReferenceReactive max)
        {
            current = currentHealth;
            max = maxHealth;
        }

        public FloatReferenceReactive currentMana = new()
        {
            useLocal = true,
            localValue = 1,
        };

        public FloatReferenceReactive maxMana = new()
        {
            useLocal = true,
            localValue = 1,
        };


        public FloatReferenceReactive speed = new()
        {
            useLocal = true,
            localValue = 1,
        };

        public FloatReference level = new()
        {
            useLocal = true,
            localValue = 1,
        };
    }
}