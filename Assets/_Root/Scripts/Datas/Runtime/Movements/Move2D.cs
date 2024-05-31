using System;
using _Root.Scripts.Datas.Runtime.Inputs;
using _Root.Scripts.Datas.Runtime.Interfaces;
using _Root.Scripts.Datas.Runtime.Variables;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Movements
{
    public abstract class Move2D : InputConsumer<Move2D>, IMove, ISpeed
    {
        [SerializeField] private Performing<InputProvider<Move2D>> manualInputProvider;
        [SerializeField] private InputProvider<Move2D> autoInputProvider;
        [SerializeField] private Performing<Vector2> direction;
        [SerializeField] private Vector2Variable worldScale;
        [Range(0, 15)] [SerializeField] protected float speed = 1f;

        public override Performing<InputProvider<Move2D>> ManualInputProvider => manualInputProvider;

        public override InputProvider<Move2D> AutoInputProvider
        {
            get => autoInputProvider;
            set => autoInputProvider = value;
        }

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
        
        protected abstract void AttachPersistentComponent();

        private void Reset()
        {
            AttachPersistentComponent();
        }
    }
}