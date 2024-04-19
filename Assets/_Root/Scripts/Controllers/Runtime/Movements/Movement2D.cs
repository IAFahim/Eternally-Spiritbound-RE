using _Root.Scripts.Datas.Runtime.Characters;
using _Root.Scripts.Datas.Runtime.Movements;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Movements
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Movement2D : Movement2DData
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Transform modelTransform;

        private static readonly Vector3 FlipScaleRight = new(1, 1, 1);
        private static readonly Vector3 FlipScaleLeft = new(-1, 1, 1);
        private MovingPlatform2D _movingPlatform;

        public bool OnAMovingPlatform => _movingPlatform;

        private void OnValidate()
        {
            GetComponentReference();
        }

        private void OnEnable()
        {
            GetComponentReference();
        }

        public void GetComponentReference()
        {
            rigidBody ??= GetComponent<Rigidbody2D>();
            modelTransform ??= GetComponent<Character>().model.transform;
        }

        private void FixedUpdate()
        {
            ApplyImpact();

            if (!Free)
            {
                return;
            }

            if (IsMoving) FlipModel(Direction);

            if (Friction > 1)
            {
                Direction = Direction / Friction;
            }

            // if we have a low friction (ice, marbles...) we lerp the speed accordingly
            if (Friction is > 0 and < 1)
            {
                Direction = Vector2.Lerp(Speed, Direction, Time.deltaTime * Friction);
            }

            Vector2 newMovement = rigidBody.position + (Direction * Speed + AddedForce) * Time.fixedDeltaTime;

            if (OnAMovingPlatform)
            {
                newMovement += (Vector2)(_movingPlatform.CurrentSpeed * Time.fixedDeltaTime);
            }

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