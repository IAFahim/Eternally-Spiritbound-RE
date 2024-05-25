using System.Collections.Generic;
using _Root.Scripts.Datas.Runtime.Attacks;
using _Root.Scripts.Datas.Runtime.LookUpTables;
using _Root.Scripts.Datas.Runtime.Variables;
using Alchemy.Inspector;
using LitMotion;
using Pancake;
using Pancake.Common;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Attacks
{
    public class AttackManager : GameComponent
    {
        public Transform firePoint;
        public StringVariable nameOrTitle;
        public AttackUnlockedLookUpTable attackUnlockedLookUpTable;

        public List<Attack> availableAttacks;
        public List<DelayHandle> AttackHandles;

        private void Awake() => availableAttacks = attackUnlockedLookUpTable.GetOrDefault(nameOrTitle);
        private void Start() => AttackHandles = new List<DelayHandle>();

        [Button]
        public void AttackRandom()
        {
            var randomIndex = Random.Range(0, availableAttacks.Count);
            var attackHandle = availableAttacks[randomIndex].Execute(Transform, firePoint.position);
            AttackHandles.Add(attackHandle);
        }

        private void Reset()
        {
            var nameOrTitleComponent = GetComponent<NameOrTitleComponent>();
            if (nameOrTitleComponent == null) nameOrTitle = nameOrTitleComponent.NameOrTitle;
        }
    }
}