using UnityEngine;

namespace _Root.Scripts.Utilities
{
    public static class DebugExt
    {
        /// <summary>
        /// Draws a gizmo sphere of the specified size and color at a position
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="size">Size.</param>
        /// <param name="color">Color.</param>
        public static void DrawGizmoPoint(Vector3 position, float size, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(position,size);
        }
    }
}