using _Root.Scripts.Controllers.Runtime.Characters;
using CleverCrow.Fluid.BTs.Trees;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Behaviors
{
    public class CharacterBehavior: MonoBehaviour
    {
        public Character character;
        public BehaviorTree behaviorTree;

        private void OnValidate()
        {
            character ??= GetComponent<Character>();
        }
    }
}