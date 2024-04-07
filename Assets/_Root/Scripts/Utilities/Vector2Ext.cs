using UnityEngine;

namespace _Root.Scripts.Utilities
{
    public static class Vector2Ext
    {
        public static string ToCurrentMax(this Vector2 vector2)
        {
            return $"{vector2.x}/{vector2.y}";
        }
    }
}