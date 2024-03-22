using _Root.Scripts.Datas.Runtime.Statistics;
using CleverCrow.Fluid.BTs.Trees;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Characters
{
    public class CharactersData: GameComponent
    {
        public string characterName;
        public CharacterType characterType;
        public FloatVariable wellBeing;
        public StatsBase stats;
        public Vector2Variable spawnPoint;
        
        public BehaviorTree behaviorTree;
        public GameObject model;
    }
}