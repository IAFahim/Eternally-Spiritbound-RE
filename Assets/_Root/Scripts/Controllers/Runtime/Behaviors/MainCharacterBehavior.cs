using _Root.Scripts.Datas.Runtime.Behaviors;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using Pancake;
using Pancake.Common;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Behaviors
{
    public class MainCharacterBehavior: CharacterBehavior
    {
        private void Start()
        {
            behaviorTree = new BehaviorTreeBuilder(gameObject)
                .Sequence()
                .Condition("IsDead", () => !character.enabled)
                .Do("Death", () =>
                {
                    Debug.Log("Death");
                    return TaskStatus.Success;
                })
                .End()
                .Build();
            App.AddListener(EUpdateMode.Update, () => behaviorTree.Tick());
        }
    }
}