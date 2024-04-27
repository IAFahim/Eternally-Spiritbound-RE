using _Root.Scripts.Datas.Runtime.Interfaces;
using _Root.Scripts.Datas.Runtime.LookUpTables;
using _Root.Scripts.Datas.Runtime.Variables;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [DefaultExecutionOrder(100)]
    public class Stats : MonoBehaviour, IHealth
    {
        public Optional<StatsLookUpTable> statsLookUpTable;
        [field: SerializeField] public Reactive<Vector2> Health { get; private set; }
        [field: SerializeField] public Reactive<Vector2> Mana { get; private set; }
        [field: SerializeField] public Reactive<float> Mood { get; private set; }
        [field: SerializeField] public Reactive<Vector2> Speed { get; private set; }
        [field: SerializeField] public Reactive<float> Level { get; private set; }
        
        private void OnEnable()
        {
            if (statsLookUpTable.Enabled)
            {
                statsLookUpTable.Value.Get(GetComponent<INameStr>().NameStr, out StatsData data);
                Set(data);
            }
        }

        private void Set(StatsData data)
        {
            Mood.Value = data.mood;
            Health.Value = data.health;
            Mana.Value = data.mana;
            Speed.Value = data.speed;
            Level.Value = data.level;
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            Health.ForceInvokeOnValueChange();
            Mana.ForceInvokeOnValueChange();
            Mood.ForceInvokeOnValueChange();
            Speed.ForceInvokeOnValueChange();
            Level.ForceInvokeOnValueChange();

#endif
        }
    }
}