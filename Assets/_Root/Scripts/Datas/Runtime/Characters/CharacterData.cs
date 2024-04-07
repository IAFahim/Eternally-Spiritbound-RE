using _Root.Scripts.Datas.Runtime.Movements;
using _Root.Scripts.Datas.Runtime.Statistics;
using CleverCrow.Fluid.BTs.Trees;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;
using UnityEngine.Serialization;

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
        public Stats stats;
        public Vector2Variable spawnPoint;
        public Movement2DData movement2D;
        [FormerlySerializedAs("healthControl")] [FormerlySerializedAs("healthController")] [FormerlySerializedAs("health")] public HealthAuthoring healthAuthoring;

        public BehaviorTree behaviorTree;
        public GameObject model;
    }
}