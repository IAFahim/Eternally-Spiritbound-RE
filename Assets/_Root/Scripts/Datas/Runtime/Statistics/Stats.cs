using _Root.Scripts.Datas.Runtime.Variables;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    public class Stats : MonoBehaviour
    {
        public FloatReferenceR loveHate = new()
        {
            useLocal = true,
            localValue = 0.5f,
        };
        
        public Vector2ReferenceR health = new()
        {
            useLocal = true,
            localValue = new Vector2(1, 1),
        };

        public ResistanceVariable resistanceVariable;
        
        public Vector2ReferenceR mana = new()
        {
            useLocal = true,
            localValue = new Vector2(1, 1),
        };
        
        public FloatReferenceR speed = new()
        {
            useLocal = true,
            localValue = 1,
        };

        public FloatReference level = new()
        {
            useLocal = true,
            localValue = 1,
        };
    }
}