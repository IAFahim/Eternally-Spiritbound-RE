using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public abstract class EffectRunner<T> : ScriptableList<T>
        where T : IEffectReference, IEffectParameter, IEffectSettings
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
                if (!data.Setting.EffectStarted && data.Setting.TickBeforeStart(Time.deltaTime)) OnApply(data);
                if (data.Setting.EffectStarted && !data.Setting.TickOnUpdate(Time.deltaTime)) Remove(data);
            }
        }

        protected abstract void OnRemove(T data);

        public new virtual void Clear()
        {
            updateEnabled = false;
            App.RemoveListener(UpdateMode.Update, Update);
            base.Clear();
        }

        public new void Add(T effect)
        {
            if (effect.Reference.AllowMultiple(effect.Setting.StackLimit) == false) return;
            OnStart(effect);
            if (Count == 0)
            {
                App.AddListener(UpdateMode.Update, Update);
                updateEnabled = true;
            }

            base.Add(effect);
        }

        public new bool Remove(T effect)
        {
            var isPresent = base.Remove(effect);
            if (isPresent)
            {
                if (Count == 0)
                {
                    App.RemoveListener(UpdateMode.Update, Update);
                    updateEnabled = false;
                }

                OnRemove(effect);
            }

            return isPresent;
        }
    }
}