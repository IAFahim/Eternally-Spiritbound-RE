using _Root.Scripts.Datas.Runtime.Brains;
using _Root.Scripts.Datas.Runtime.Interfaces;
using Pancake;
using Pancake.Common;
using Pancake.PlayerLoop;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Activities
{
    public abstract class FollowComponent : AIBrain, IDebug, IUpdate
    {
        [field: SerializeReference] public bool DebugEnabled { get; set; } = true;

        [SerializeField] private bool haasAllReference;

        [SerializeField] protected GameComponent target;
        [SerializeField] private Vector2 direction;

        private IMove _moveRef;

        protected bool HasAllReference
        {
            get => haasAllReference;
            set => haasAllReference = value;
        }
        
        public GameComponent Target
        {
            get => target;
            set
            {
                target = value;
                HasAllReference = target != null;
                if (_moveRef != null) Direction = Vector2.zero;
            }
        }

        protected Vector2 Direction
        {
            get => direction;
            set => _moveRef.Direction = direction = value;
        }

        public virtual void OnEnable()
        {
            _moveRef ??= GetComponent<IMove>();
            HasAllReference = _moveRef != null && target != null;
            App.AddListener(EUpdateMode.Update, OnUpdate);
        }

        public virtual void OnDisable()
        {
            enabled = false;
            App.RemoveListener(EUpdateMode.Update, OnUpdate);
        }

        public void OnUpdate()
        {
            if (HasAllReference) Follow();
        }

        public abstract bool Condition();

        protected abstract void Follow();

        protected virtual void OnValidate()
        {
            Target = target;
        }
    }
}