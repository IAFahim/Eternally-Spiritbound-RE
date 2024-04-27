using _Root.Scripts.Controllers.Runtime.Characters;
using _Root.Scripts.Datas.Runtime.LookUpTables;
using _Root.Scripts.Datas.Runtime.Statistics;
using _Root.Scripts.Datas.Runtime.Variables;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Statistics
{
    [RequireComponent(typeof(Character))]
    public class Stats : MonoBehaviour
    {
        public Character character;
        public Optional<StatsLookUpTable> statsLookUpTable;
        public Reactive<float> happiness;
        public Reactive<Vector2> health;
        public Reactive<Vector2> mana;
        public Reactive<Vector2> speed;
        public Reactive<float> level;
        
        private void OnEnable()
        {
            if (statsLookUpTable.Enabled)
            {
                statsLookUpTable.Value.Get(character.nameOfCharacter, out StatsData data);
                Set(data);
            }
        }

        private void Reset()
        {
            character = GetComponent<Character>();
        }
        
        private void Set(StatsData data)
        {
            happiness.Value = data.happiness;
            health.Value = data.health;
            mana.Value = data.mana;
            speed.Value = data.speed;
            level.Value = data.level;
        }
        
    }
}