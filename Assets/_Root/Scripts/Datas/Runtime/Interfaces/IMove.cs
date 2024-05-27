using _Root.Scripts.Datas.Runtime.Variables;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Interfaces
{
    public interface IMove
    { 
        public Performing<Vector2> Direction { get; set; }
        public void Move();
    }
}