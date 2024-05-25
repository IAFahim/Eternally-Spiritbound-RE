using System;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Interfaces
{
    public interface IMovementDirection
    {
        public bool IsReceivingMoveInput { get; set; }
        public Vector2 Direction { get; set; }
    }
}