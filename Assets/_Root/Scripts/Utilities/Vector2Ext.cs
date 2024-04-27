using UnityEngine;

namespace _Root.Scripts.Utilities
{
    public static class Vector2Ext
    {
        public static float Current(this Vector2 vector2) => vector2.x;
        public static float Max(this Vector2 vector2) => vector2.y;
        public static float GetPercentage(this Vector2 vector2) => vector2.x / vector2.y;
        public static string ToCurrentMax(this Vector2 vector2) => $"{vector2.x}/{vector2.y}";
    }
}