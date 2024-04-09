using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Behaviors
{
    public class MainCharacterBehavior: CharacterBehavior
    {
        private void Start()
        {
            behaviorTree = new BehaviorTreeBuilder(gameObject)
                .Sequence()
                .Condition("IsDead", () => character.healthAuthoring.IsDead)
                .Do("Death", () =>
                {
                    Debug.Log("Death");
                    return TaskStatus.Success;
                })
                .End()
                .Build();

            App.AddListener(UpdateMode.Update, () => behaviorTree.Tick());
        }
    }
}