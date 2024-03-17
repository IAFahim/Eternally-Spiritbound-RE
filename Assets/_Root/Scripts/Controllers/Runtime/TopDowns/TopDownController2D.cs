using _Root.Scripts.Controllers.Runtime.TopDowns.Bases;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.TopDowns
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class TopDownController2D : TopDownController
    {
        public Rigidbody2D rigidBody;
        private Vector3 _impact;
        private MovingPlatform2D _movingPlatform;
        public Vector3 AddedForce;
        
        public override bool OnAMovingPlatform { get { return _movingPlatform; } }

        private void OnValidate()
        {
            rigidBody ??= GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {

            ApplyImpact();

            if (!FreeMovement)
            {
                return;
            }

            if (Friction > 1)
            {
                CurrentMovement = CurrentMovement / Friction;
            }
            
            // if we have a low friction (ice, marbles...) we lerp the speed accordingly
            if (Friction is > 0 and < 1)
            {
                CurrentMovement = Vector3.Lerp(Speed, CurrentMovement, Time.deltaTime * Friction);
            }
            
            Vector2 newMovement = rigidBody.position + (Vector2)(CurrentMovement + AddedForce) * Time.fixedDeltaTime;
            
            if (OnAMovingPlatform)
            {
                newMovement += (Vector2)_movingPlatform.CurrentSpeed * Time.fixedDeltaTime;
            }
            rigidBody.MovePosition(newMovement);
        }
        
        /// <summary>
        /// Applies the current impact
        /// </summary>
        private void ApplyImpact()
        {
            if (_impact.magnitude > 0.2f)
            {
                rigidBody.AddForce(_impact);
            }
            _impact = Vector3.Lerp(_impact, Vector3.zero, 5f * Time.deltaTime);
        }
    }
}