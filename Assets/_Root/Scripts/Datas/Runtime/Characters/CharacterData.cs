using _Root.Scripts.Datas.Runtime.Movements;
using _Root.Scripts.Datas.Runtime.Statistics;
using CleverCrow.Fluid.BTs.Trees;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Characters
{
    public class CharacterData: GameComponent
    {
        public string characterName;
        public CharacterType characterType;
        public FloatVariable loveHate;
        public Stats stats;
        public Vector2Variable spawnPoint;
        
        public Movement2DData movement2D;
        
        public BehaviorTree behaviorTree;
        public GameObject model;

    }
}