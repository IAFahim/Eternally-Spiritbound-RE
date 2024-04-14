using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public abstract class EffectRunner<T> : ScriptableList<T>
        where T : IEffectReference, IEffectSettings
    {
        public bool updateEnabled;
        protected abstract void OnStart(T frictionEffectRef);
        protected abstract void OnApply(T frictionEffectRef);

        protected virtual void Update()
        {
            if (updateEnabled == false) return;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                T data = list[i];
                if (!data.Settings.EffectStarted && data.Settings.TickBeforeStart(Time.deltaTime)) OnApply(data);
                if (data.Settings.EffectStarted && !data.Settings.TickOnUpdate(Time.deltaTime)) Remove(data);
            }
        }

        protected abstract void OnRemove(T data);

        public new virtual void Clear()
        {
            updateEnabled = false;
            App.RemoveListener(UpdateMode.Update, Update);
            base.Clear();
        }

        public new void Add(T data)
        {
            if (data.Reference.AllowMultiple(data.Settings.Stackable) == false) return;
            OnStart(data);
            if (Count == 0)
            {
                App.AddListener(UpdateMode.Update, Update);
                updateEnabled = true;
            }

            base.Add(data);
        }

        public new bool Remove(T data)
        {
            var isPresent = base.Remove(data);
            if (isPresent)
            {
                if (Count == 0)
                {
                    App.RemoveListener(UpdateMode.Update, Update);
                    updateEnabled = false;
                }

                OnRemove(data);
            }

            return isPresent;
        }
    }
}