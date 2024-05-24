using _Root.Scripts.Datas.Runtime.Statistics;
using _Root.Scripts.Datas.Runtime.Variables;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.LookUpTables
{
    public class StatsLookUpTable : SaveAbleLookUpTable<StringVariable, StatsData>
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