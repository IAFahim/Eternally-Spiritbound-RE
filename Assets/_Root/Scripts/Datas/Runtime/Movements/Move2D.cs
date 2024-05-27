using _Root.Scripts.Datas.Runtime.Interfaces;
using _Root.Scripts.Datas.Runtime.Variables;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Movements
{
    public abstract class Move2D : GameComponent, IMove, ISpeed
    {
        [SerializeField] private Performing<Vector2> direction;
        [SerializeField] private Vector2Variable worldScale;
        [Range(0, 15)] [SerializeField] protected float speed = 1f;

        public Performing<Vector2> Direction
        {
            get => direction;
            set => direction = value;
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public Vector2 SpeedVector => speed * worldScale.Value;

        public abstract void Move();
    }
}