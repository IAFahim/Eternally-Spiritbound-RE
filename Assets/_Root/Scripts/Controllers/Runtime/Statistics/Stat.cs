using _Root.Scripts.Controllers.Runtime.Characters;
using _Root.Scripts.Datas.Runtime.LookUpTables;
using _Root.Scripts.Datas.Runtime.Statistics;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Statistics
{
    [DefaultExecutionOrder(-1)]
    public class Stat: MonoBehaviour
    {
        public Character character;
        public StatsLookUpTable statsLookUpTable;
        public StatsData statsData;
        
        private void OnEnable()
        {
            statsLookUpTable.Get(character.nameOfCharacter, out statsData);
        }
    }
}