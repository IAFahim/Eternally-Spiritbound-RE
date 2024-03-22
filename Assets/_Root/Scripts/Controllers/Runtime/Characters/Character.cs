using _Root.Scripts.Controllers.Runtime.Events;
using _Root.Scripts.Controllers.Runtime.Movements;
using _Root.Scripts.Datas.Runtime.Characters;
using _Root.Scripts.Datas.Runtime.Statistics;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Characters
{
    [RequireComponent(typeof(Movement2D))]
    public class Character : CharactersData
    {
        public HealthBase healthBase;
        public Movement2D movement2D;
        public SwapCharacterEvent swapCharacterEvent;

        private void OnValidate()
        {
            healthBase ??= GetComponent<HealthBase>();
            movement2D ??= GetComponent<Movement2D>();
        }

        private void Awake()
        {
            behaviorTree = new BehaviorTreeBuilder(gameObject)
                .Sequence()
                .Condition("IsDead", () => healthBase.IsDead)
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