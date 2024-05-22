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

        private IDirection directionRef;

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
                if (directionRef != null) Direction = Vector2.zero;
            }
        }

        protected Vector2 Direction
        {
            get => direction;
            set => directionRef.Direction = direction = value;
        }

        public virtual void OnEnable()
        {
            directionRef ??= GetComponent<IDirection>();
            HasAllReference = directionRef != null && target != null;
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