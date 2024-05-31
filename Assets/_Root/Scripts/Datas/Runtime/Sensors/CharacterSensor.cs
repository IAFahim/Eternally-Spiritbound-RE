using System.Collections.Generic;
using _Root.Scripts.Datas.Runtime.Characters;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Sensors
{
    public class CharacterSensor : Sensor
    {
        public Character closestCharacter;
        public Character farthestCharacter;
        public List<Character> characters;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var character = other.GetComponent<Character>();
            if (character == null) return;

            characters.Add(character);
            if (closestCharacter == null || Vector2.Distance(Transform.position, character.Transform.position) <
                Vector2.Distance(Transform.position, closestCharacter.Transform.position))
            {
                closestCharacter = character;
            }

            if (farthestCharacter == null || Vector2.Distance(Transform.position, character.Transform.position) >
                Vector2.Distance(Transform.position, farthestCharacter.Transform.position))
            {
                farthestCharacter = character;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var character = other.GetComponent<Character>();
            if (character == null) return;

            characters.Remove(character);
            if (character == closestCharacter)
            {
                closestCharacter = null;
            }

            if (character == farthestCharacter)
            {
                farthestCharacter = null;
            }
        }
    }
}