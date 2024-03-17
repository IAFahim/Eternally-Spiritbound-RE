using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.TopDowns
{
    public class Stats: MonoBehaviour
    {
        public Character character;
        private void OnValidate()
        {
            character ??= GetComponent<Character>();
        }
    }
}