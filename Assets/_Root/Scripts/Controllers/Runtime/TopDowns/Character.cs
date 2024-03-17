using _Root.Scripts.Datas.Events;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.TopDowns
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Movement2D), typeof(Stats))]
    public class Character : GameComponent
    {
        public SwapCharacterEvent swapCharacterEvent;
        public Rigidbody2D rigidBody;
        public Movement2D movement2D;
        public Stats stats;

        private void OnValidate()
        {
            rigidBody ??= GetComponent<Rigidbody2D>();
            movement2D ??= GetComponent<Movement2D>();
            stats ??= GetComponent<Stats>();
        }

        public void SwapCharacter(Character character)
        {
            swapCharacterEvent.Raise(character);
        }
    }
}