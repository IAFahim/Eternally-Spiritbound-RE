using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime
{
    ///<summary>
    ///This class describes a node on an MMPath
    ///</summary>
    [System.Serializable]
    public class PathMovementElement
    {
        /// the point that make up the path the object will follow
        public Vector3 pathElementPosition;
        /// a delay (in seconds) associated to each node
        public float delay;
    }
}