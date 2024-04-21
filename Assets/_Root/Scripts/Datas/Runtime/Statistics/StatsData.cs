using System;
using _Root.Scripts.Datas.Runtime.Variables;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [Serializable]
    public class StatsData
    {
        [SerializeField] private float happiness = 0.5f;
        [SerializeField] private Vector2 health = new(5, 5);
        [SerializeField] private Vector2 mana = new(3, 3);
        [SerializeField] private Vector2 speed = new(1, 1);
        [SerializeField] private float level = 1;

        public Action<StatsData> OnValueChange;

        public float Happiness
        {
            get => happiness;
            set
            {
                happiness = value;
                OnValueChange?.Invoke(this);
            }
        }

        public Vector2 Health
        {
            get => health;
            set
            {
                health = value;
                OnValueChange?.Invoke(this);
            }
        }

        public Vector2 Mana
        {
            get => mana;
            set
            {
                mana = value;
                OnValueChange?.Invoke(this);
            }
        }

        public Vector2 Speed
        {
            get => speed;
            set
            {
                speed = value;
                OnValueChange?.Invoke(this);
            }
        }

        public float Level
        {
            get => level;
            set
            {
                level = value;
                OnValueChange?.Invoke(this);
            }
        }
    }
}