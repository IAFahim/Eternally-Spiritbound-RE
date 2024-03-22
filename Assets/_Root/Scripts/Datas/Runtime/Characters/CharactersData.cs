using _Root.Scripts.Datas.Runtime.Statistics;
using CleverCrow.Fluid.BTs.Trees;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Characters
{
    public class CharactersData: GameComponent
    {
        public FloatVariable wellBeing;
        public Health health;
        public Stats stats;
        public BehaviorTree behaviorTree;
        public GameObject model;
        public Rigidbody2D rigidBody;
    }
}