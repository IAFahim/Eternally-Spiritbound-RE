using _Root.Scripts.Datas.Runtime.Movements;
using _Root.Scripts.Datas.Runtime.Statistics;
using _Root.Scripts.Datas.Runtime.Variables;
using CleverCrow.Fluid.BTs.Trees;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Characters
{
    public class CharacterData : GameComponent
    {
        public StringReference characterName = new()
        {
            useLocal = true,
            localValue = "Character",
        };

        public CharacterType characterType;

        public FloatReferenceReactive loveHate = new()
        {
            useLocal = true,
            localValue = 0.5f,
        };

        public Stats stats;
        public Vector2Variable spawnPoint;
        public Movement2DData movement2D;
        public Health health;

        public BehaviorTree behaviorTree;
        public GameObject model;
    }
}