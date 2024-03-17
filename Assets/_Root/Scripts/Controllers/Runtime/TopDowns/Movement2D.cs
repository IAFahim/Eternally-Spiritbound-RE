using _Root.Scripts.Datas;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.TopDowns
{
    public class Movement2D : GameComponent, IGravity
    {
        public Character character;
        
        [field: SerializeReference] public bool GravityActive { get; set; } = true;
        [field: SerializeReference] public float Gravity { get; set; } = 40f;

        [field: SerializeReference] public bool Free { get; set; } = true;
        [field: SerializeReference] public float Friction { get; set; } = 1f;
        [field: SerializeReference] public Vector2 Current { get; set; }
        [field: SerializeReference] public Vector2 Speed { get; set; }
        
        private Vector2 _impact;
        private MovingPlatform2D _movingPlatform;
        public Vector2 AddedForce;
        public bool OnAMovingPlatform => _movingPlatform;

        private void OnValidate()
        {
            character ??= GetComponent<Character>();
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
                Current = Current / Friction;
            }

            // if we have a low friction (ice, marbles...) we lerp the speed accordingly
            if (Friction is > 0 and < 1)
            {
                Current = Vector3.Lerp(Speed, Current, Time.deltaTime * Friction);
            }

            Vector2 newMovement = character.rigidBody.position + (Current + AddedForce) * Time.fixedDeltaTime;

            if (OnAMovingPlatform)
            {
                newMovement += (Vector2)(_movingPlatform.CurrentSpeed * Time.fixedDeltaTime);
            }

            character.rigidBody.MovePosition(newMovement);
        }

        /// <summary>
        /// Applies the current impact
        /// </summary>
        private void ApplyImpact()
        {
            if (_impact.magnitude > 0.2f)
            {
                character.rigidBody.AddForce(_impact);
            }

            _impact = Vector2.Lerp(_impact, Vector2.zero, 5f * Time.deltaTime);
        }
    }
}