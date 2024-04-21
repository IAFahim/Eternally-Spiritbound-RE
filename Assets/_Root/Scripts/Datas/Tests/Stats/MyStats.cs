using _Root.Scripts.Datas.Runtime.Characters;
using _Root.Scripts.Datas.Runtime.Statistics;
using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Datas.Tests.Stats
{
    [RequireComponent(typeof(Character))]
    public class MyStats: MonoBehaviour
    {
        public Character character;
        public StatsLookUpTable statsLookUpTable;
        public StatsData statsData;
        private void OnEnable()
        {
            statsLookUpTable.Get(character.characterName.Value, out statsData);
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
            statsData.OnValueChange?.Invoke(statsData);
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