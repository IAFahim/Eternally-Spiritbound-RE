using _Root.Scripts.Datas.Runtime.Variables;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [CreateAssetMenu(menuName = "Stats", fileName = "ScriptAbles/Stats", order = 0)]
    public class Stats : ScriptableObject
    {
        public FloatLimit health;
        public FloatLimit mana;
        
        
        public FloatReference speed;
        public FloatReference level;
    }
}