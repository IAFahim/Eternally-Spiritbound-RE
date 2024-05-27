using _Root.Scripts.Controllers.Runtime.Movements;
using _Root.Scripts.Datas.Runtime;
using _Root.Scripts.Datas.Runtime.Interfaces;
using _Root.Scripts.Datas.Runtime.Variables;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Characters
{
    [SelectionBase]
    [RequireComponent(typeof(MovementLookAround2D))]
    [DefaultExecutionOrder(-10)]
    public class Character : GameComponent
    {
        
        public Vector2Reference spawnPoint = new() { useLocal = true };
        public Reactive<CharacterType> type;
        public GameObject model;
    }
}