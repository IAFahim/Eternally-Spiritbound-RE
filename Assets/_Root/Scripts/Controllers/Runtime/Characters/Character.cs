using _Root.Scripts.Datas.Runtime.Characters;
using _Root.Scripts.Datas.Runtime.Movements;
using _Root.Scripts.Datas.Runtime.Statistics;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Characters
{
    [RequireComponent(typeof(Movement2DData))]
    public class Character : CharacterData
    {
        public MainCharacterSelectEvent swapCharacterSelectEventEvent;

        private void OnValidate()
        {
            stats ??= GetComponent<Stats>();
            healthAuthoring ??= GetComponent<HealthAuthoring>();
            movement2D ??= GetComponent<Movement2DData>();
        }

        private void Awake()
        {
            behaviorTree = new BehaviorTreeBuilder(gameObject)
                .Sequence()
                .Condition("IsDead", () => healthAuthoring.IsDead)
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