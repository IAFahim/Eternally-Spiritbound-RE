using System;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [Serializable]
    public class StatsData
    {
        public Vector2 health = new(5, 5);
        public Vector2 mana = new(3, 3);
        public float mood = 0.5f;
        public Vector2 speed = new(1, 1);
        public float level = 1;
    }
}