using _Root.Scripts.Datas.Runtime.Statistics;
using CleverCrow.Fluid.BTs.Trees;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Characters
{
    public class CharactersData: GameComponent
    {
        public BehaviorTree behaviorTree;
        public Health health;
        public GameObject model;
        public Rigidbody2D rigidBody;
        public Stats stats;
    }
}