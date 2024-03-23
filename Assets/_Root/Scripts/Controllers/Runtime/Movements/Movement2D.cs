using _Root.Scripts.Datas.Runtime.Movements;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Movements
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement2D : Movement2DData
    {
        [SerializeField] private Rigidbody2D rigidBody;
        private MovingPlatform2D _movingPlatform;
        public Rigidbody2D RigidBody => rigidBody;
        public bool OnAMovingPlatform => _movingPlatform;

        private void OnValidate()
        {
            rigidBody ??= GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            ApplyImpact();

            if (!Free)
            {
                return;
            }

            if (Friction > 1)
            {
                Direction = Direction / Friction;
            }

            // if we have a low friction (ice, marbles...) we lerp the speed accordingly
            if (Friction is > 0 and < 1)
            {
                Direction = Vector3.Lerp(Speed, Direction, Time.deltaTime * Friction);
            }

            Vector2 newMovement = rigidBody.position + (Direction + AddedForce) * Time.fixedDeltaTime;

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
    }
}