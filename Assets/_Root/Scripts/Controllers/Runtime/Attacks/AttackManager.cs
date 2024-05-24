using System.Collections.Generic;
using _Root.Scripts.Datas.Runtime.Attacks;
using _Root.Scripts.Datas.Runtime.LookUpTables;
using _Root.Scripts.Datas.Runtime.Variables;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Attacks
{
    public class AttackManager : MonoBehaviour
    {
        public StringVariable nameOrTitle;
        public Optional<AttackUnlockedLookUpTable> attackUnlockedLookUpTable;
        public List<Attack> unlockedAttacks;

        private void Awake()
        {
            if (attackUnlockedLookUpTable.Enabled) unlockedAttacks = attackUnlockedLookUpTable.Value.GetOrDefault(nameOrTitle);
        }

        private void Reset()
        {
            var nameOrTitleComponent = GetComponent<NameOrTitleComponent>();
            if (nameOrTitleComponent == null) nameOrTitle = nameOrTitleComponent.NameOrTitle;
        }
    }
}