using System;
using _Root.Scripts.Controllers.Runtime.Characters;
using _Root.Scripts.Datas.Runtime.LookUpTables;
using _Root.Scripts.Datas.Runtime.Statistics;
using Pancake;
using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Controllers.Tests.Stats
{
    [RequireComponent(typeof(Character))]
    public class MyStats : MonoBehaviour
    {
        public Character character;
        public Optional<StatsLookUpTable> statsLookUpTable;
        public StatsData statsData;

        private void OnEnable()
        {
            if (statsLookUpTable.Enabled) statsLookUpTable.Value.Get(character.nameOfCharacter, out statsData);
            statsData.OnValueChange += UpdateStats;
        }

        private void OnDisable()
        {
            statsData.OnValueChange -= UpdateStats;
        }

        private void UpdateStats(StatsData obj)
        {
            Debug.Log("Stats updated");
        }

        private void OnValidate()
        {
            statsData.ForceNotifyChange();
        }

        [Button]
        public void ChangeStats()
        {
            statsData.Happiness = 0.7f;
        }

        private void Reset()
        {
            character = GetComponent<Character>();
        }
    }
}