using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Interfaces
{
    public interface IDirection
    {
        public Vector2 Direction { get; set; }
        public bool IsReceivingMoveInput { get; set; }
    }
}