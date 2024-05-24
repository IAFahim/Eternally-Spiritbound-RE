using _Root.Scripts.Datas.Runtime.LookUpTables;
using _Root.Scripts.Datas.Runtime.Variables;
using Alchemy.Inspector;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [DefaultExecutionOrder(100)]
    public class Stats : MonoBehaviour, IHealth, ILevel
    {
        public StringVariable nameOrTitle;
        public Optional<StatsLookUpTable> statsLookUpTable;
        [field: SerializeField] public Reactive<Vector2> Health { get; set; }
        [field: SerializeField] public Reactive<Vector2> Mana { get; private set; }
        [field: SerializeField] public Reactive<float> Mood { get; private set; }
        [field: SerializeField] public Reactive<Vector2> Speed { get; private set; }
        [field: SerializeField] public Reactive<float> Level { get; set; }


        private void OnEnable()
        {
            Fetch();
        }

        [Button]
        private void Fetch()
        {
            if (statsLookUpTable.Enabled)
            {
                Set(statsLookUpTable.Value.GetOrDefault(nameOrTitle));
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
        
        private void Reset()
        {
            var nameOrTitleComponent = GetComponent<NameOrTitleComponent>();
            if (nameOrTitleComponent == null) nameOrTitle = nameOrTitleComponent.NameOrTitle;
        }
    }
}