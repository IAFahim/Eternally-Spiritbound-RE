using _Root.Scripts.Controllers.Runtime.Characters;
using _Root.Scripts.Datas.Runtime.Movements;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Movements
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class MovementLookAround2D : Move2D, IFriction
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private float friction = 1f;

        public Vector2 impact;
        public Vector2 addedForce;

        private static readonly Vector3 FlipScaleRight = new(1, 1, 1);
        private static readonly Vector3 FlipScaleLeft = new(-1, 1, 1);

        public float Friction
        {
            get => friction;
            set => friction = value;
        }
        
        
        private void OnValidate()
        {
            GetComponentReference();
        }

        private void OnEnable()
        {
            GetComponentReference();
        }
        
        private void OnDisable()
        {
        }

        public void GetComponentReference()
        {
            rigidBody ??= GetComponent<Rigidbody2D>();
            modelTransform ??= GetComponent<Character>().model.transform;
        }

        private void FixedUpdate()
        {
            Move();
        }
        
        public override void Move()
        {
            ApplyImpact();

            if (Friction > 1)
            {
                Direction = Direction.Value / Friction;
            }
            
            // if we have a low friction (ice, marbles...) we lerp the speed accordingly
            if (Friction is > 0 and < 1)
            {
                Direction = Vector2.Lerp(SpeedVector, Direction, Time.deltaTime * Friction);
            }
            
            Vector2 newMovement = rigidBody.position + (Direction * SpeedVector + addedForce) * Time.fixedDeltaTime;
            
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
        public void FlipModel(Vector3 flipDirection)
        {
            if (flipDirection.x == 0) return;
            bool isRight = !(flipDirection.x < 0);
            modelTransform.localScale = isRight ? (FlipScaleRight) : FlipScaleLeft;
        }

        
    }
}