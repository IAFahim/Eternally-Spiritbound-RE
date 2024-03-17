using System;
using _Root.Scripts.Controllers.Runtime.Events;
using _Root.Scripts.Controllers.Runtime.Movements;
using _Root.Scripts.Datas.Runtime.Characters;
using _Root.Scripts.Datas.Runtime.Statistics;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using CleverCrow.Fluid.BTs.Trees;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Characters.Base
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Movement2D), typeof(Stats))]
    public class Character : CharactersData
    {
        public SwapCharacterEvent swapCharacterEvent;
        public Movement2D movement2D;

        private void OnValidate()
        {
            rigidBody ??= GetComponent<Rigidbody2D>();
            movement2D ??= GetComponent<Movement2D>();
            stats ??= GetComponent<Stats>();
            health ??= GetComponent<Health>();
        }

        private void Awake()
        {
            behaviorTree = new BehaviorTreeBuilder(gameObject)
                .Sequence()
                .Condition("IsDead", () => health.IsDead)
                .Do("Death", () =>
                {
                    Debug.Log("Death");
                    return TaskStatus.Success;
                })
                .End()
                .Build();
            
            App.AddListener(UpdateMode.Update, () => behaviorTree.Tick());
        }

        public void SwapCharacter(Character character)
        {
            swapCharacterEvent.Raise(character);
        }
    }
}