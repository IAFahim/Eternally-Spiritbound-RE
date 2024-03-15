using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Root.Scripts.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TopDownController2D : TopDownController
    {
        public Rigidbody2D _rigidBody;
        public Vector2 direction;
        protected Vector3 _impact;
        protected MovingPlatform2D _movingPlatform;
        [Tooltip("the current added force, to be added to the character's movement")]
        public Vector3 AddedForce;
        
        ///  whether or not the character is on a moving platform
        public override bool OnAMovingPlatform { get { return _movingPlatform; } }

        private void OnValidate()
        {
            _rigidBody ??= GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CurrentMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        protected void FixedUpdate()
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
            
            Vector2 newMovement = _rigidBody.position + (Vector2)(CurrentMovement + AddedForce) * Time.fixedDeltaTime;
            
            if (OnAMovingPlatform)
            {
                newMovement += (Vector2)_movingPlatform.CurrentSpeed * Time.fixedDeltaTime;
            }
            _rigidBody.MovePosition(newMovement);
        }
        
        /// <summary>
        /// Applies the current impact
        /// </summary>
        protected virtual void ApplyImpact()
        {
            if (_impact.magnitude > 0.2f)
            {
                _rigidBody.AddForce(_impact);
            }
            _impact = Vector3.Lerp(_impact, Vector3.zero, 5f * Time.deltaTime);
        }
    }
}