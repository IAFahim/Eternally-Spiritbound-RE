using _Root.Scripts.Datas.Runtime.Variables;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    public interface IHealth
    {
        Reactive<Vector2> Health { get; }
    }
}