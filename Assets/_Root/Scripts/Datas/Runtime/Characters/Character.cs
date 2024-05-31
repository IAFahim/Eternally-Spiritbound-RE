using _Root.Scripts.Datas.Runtime.Variables;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Characters
{
    [SelectionBase]
    [DefaultExecutionOrder(-10)]
    public class Character : GameComponent
    {
        public Vector2Reference spawnPoint = new() { useLocal = true };
        public Reactive<CharacterType> type;
        public GameObject model;
    }
}