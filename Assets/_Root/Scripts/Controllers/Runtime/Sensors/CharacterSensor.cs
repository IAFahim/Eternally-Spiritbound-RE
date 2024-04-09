using System.Collections.Generic;
using _Root.Scripts.Datas.Runtime.Characters;
using _Root.Scripts.Datas.Runtime.Sensors;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Sensors
{
    public class CharacterSensor: Sensor
    {
        public Character closestCharacter;
        public Character farthestCharacter;
        public List<Character> characters;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var character = other.GetComponent<Character>();
            if (character == null) return;
            
            characters.Add(character);
            if (closestCharacter == null || Vector2.Distance(Transform.position, character.Transform.position) < Vector2.Distance(Transform.position, closestCharacter.Transform.position))
            {
                closestCharacter = character;
            }
            if (farthestCharacter == null || Vector2.Distance(Transform.position, character.Transform.position) > Vector2.Distance(Transform.position, farthestCharacter.Transform.position))
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
        
        // void OnTriggerEnter2D(Collider2D other) {
        //     int currCount;
        //     if (!colliderCount.TryGetValue(other, out currCount)) {
        //         AddCollider(other, true);
        //         currCount = 0;
        //     }
        //     colliderCount[other] = currCount + 1;
        // }
        //
        // void OnTriggerExit2D(Collider2D other) {
        //     int currCount;
        //     if (colliderCount.TryGetValue(other, out currCount)) {
        //         if (currCount == 1) {
        //             colliderCount.Remove(other);
        //             RemoveCollider(other, true);
        //         } else {
        //             colliderCount[other] = currCount - 1;
        //         }
        //     }
        // }
    }
}