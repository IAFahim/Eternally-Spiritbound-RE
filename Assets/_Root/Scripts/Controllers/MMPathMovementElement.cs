using UnityEngine;

namespace _Root.Scripts.Controllers
{
    [System.Serializable]
    /// <summary>
    /// This class describes a node on an MMPath
    /// </summary>
    public class MMPathMovementElement
    {
        /// the point that make up the path the object will follow
        public Vector3 PathElementPosition;
        /// a delay (in seconds) associated to each node
        public float Delay;
    }
}