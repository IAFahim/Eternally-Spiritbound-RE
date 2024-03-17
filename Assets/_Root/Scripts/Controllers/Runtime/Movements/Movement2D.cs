using _Root.Scripts.Controllers.Runtime.Characters.Base;
using _Root.Scripts.Datas.Runtime.Movements;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Movements
{
    public class Movement2D : MovementData2D
    {
        public Character character;
        private MovingPlatform2D _movingPlatform;
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
            if (impact.magnitude > 0.2f)
            {
                character.rigidBody.AddForce(impact);
            }

            impact = Vector2.Lerp(impact, Vector2.zero, 5f * Time.deltaTime);
        }
    }
}