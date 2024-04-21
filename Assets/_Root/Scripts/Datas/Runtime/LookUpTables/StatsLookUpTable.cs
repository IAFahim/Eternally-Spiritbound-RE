using _Root.Scripts.Datas.Runtime.Statistics;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.LookUpTables
{
    public class StatsLookUpTable : SaveAbleLookUpTable<string, StatsData>
    {
        [ContextMenu("Save")]
        public override void Save()
        {
            base.Save();
        }

        [ContextMenu("Load")]
        public override void Load()
        {
            base.Load();
        }
    }
}