using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [CreateAssetMenu(menuName = "StatsBase", fileName = "ScriptAbles/StatsBase", order = 0)]
    public class StatsBase : ScriptableObject
    {
        public FloatReference manaCurrent;
        public FloatReference manaMax;
        public FloatReference speed;
        public FloatReference experience;
    }
}