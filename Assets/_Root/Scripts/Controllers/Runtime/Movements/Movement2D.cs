using _Root.Scripts.Controllers.Runtime.Characters;
using _Root.Scripts.Datas.Runtime.Interfaces;
using _Root.Scripts.Datas.Runtime.Lists;
using _Root.Scripts.Datas.Runtime.Movements;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Root.Scripts.Controllers.Runtime.Movements
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Movement2D : GameComponent, IMovementDirection, IFriction
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Transform modelTransform;
        public MovementDirectionScriptableList movementDirectionConsumerList;
        
        [SerializeField] private bool isReceivingMoveInput;
        [SerializeField] private Vector2 direction;
        [SerializeField] private bool gravityActive = true;
        [SerializeField] private float friction = 1f;
        [SerializeField] private float gravity = 1f;
        [SerializeField] private float speed = 1f;
        [SerializeField] private Vector2Variable worldScale;

        public Vector2 impact;
        public Vector2 addedForce;

        private static readonly Vector3 FlipScaleRight = new(1, 1, 1);
        private static readonly Vector3 FlipScaleLeft = new(-1, 1, 1);


        public bool IsReceivingMoveInput
        {
            get => isReceivingMoveInput;
            set => isReceivingMoveInput = value;
        }

        public Vector2 Direction
        {
            get => direction;
            set => direction = value;
        }

        public float Friction
        {
            get => friction;
            set => friction = value;
        }
        public Vector2 Speed => speed * worldScale.Value;
        
        
        private void OnValidate()
        {
            GetComponentReference();
        }

        private void OnEnable()
        {
            GetComponentReference();
            movementDirectionConsumerList.Add(this);
        }
        
        private void OnDisable()
        {
            movementDirectionConsumerList.Remove(this);
        }

        public void GetComponentReference()
        {
            rigidBody ??= GetComponent<Rigidbody2D>();
            modelTransform ??= GetComponent<Character>().model.transform;
        }

        private void FixedUpdate()
        {
            ApplyImpact();

            if (IsReceivingMoveInput) FlipModel(Direction);

            if (Friction > 1)
            {
                Direction = Direction / Friction;
            }

            // if we have a low friction (ice, marbles...) we lerp the speed accordingly
            if (Friction is > 0 and < 1)
            {
                Direction = Vector2.Lerp(Speed, Direction, Time.deltaTime * Friction);
            }

            Vector2 newMovement = rigidBody.position + (Direction * Speed + addedForce) * Time.fixedDeltaTime;

            rigidBody.MovePosition(newMovement);
        }

        /// <summary>
        /// Applies the current impact
        /// </summary>
        private void ApplyImpact()
        {
            if (impact.magnitude > 0.2f)
            {
                rigidBody.AddForce(impact);
            }

            impact = Vector2.Lerp(impact, Vector2.zero, 5f * Time.deltaTime);
        }

        /// <summary>
        /// Flips the model only, no impact on weapons or attachments
        /// </summary>
        public void FlipModel(Vector3 direction)
        {
            if (direction.x == 0) return;
            bool isRight = !(direction.x < 0);
            modelTransform.localScale = isRight ? (FlipScaleRight) : FlipScaleLeft;
        }
    }
}