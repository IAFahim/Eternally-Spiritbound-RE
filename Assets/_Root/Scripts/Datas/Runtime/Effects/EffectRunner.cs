using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public abstract class EffectRunner<T> : ScriptableList<T>
        where T : IEffectReference, IEffectParameter, IEffectSettings
    {
        public bool updateEnabled;
        protected abstract void OnAdd(T effect);
        protected abstract void OnCycle(T effect);
        protected abstract void OnRemove(T effect);

        public new void Add(T effect)
        {
            if (effect.Reference.AllowMultiple(effect.Setting.StackLimit) == false) return;
            effect.Reference.StackCount++;
            OnAdd(effect);
            base.Add(effect);
            if (Count == 1)
            {
                App.AddListener(UpdateMode.Update, Update);
                updateEnabled = true;
            }
        }

        protected virtual void Update()
        {
            if (updateEnabled == false) return;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                T data = list[i];
                if (!data.Setting.TickOnUpdate(Time.deltaTime)) OnCycleChange(data);
            }
        }
        
        private void OnCycleChange(T effect)
        {
            if (--effect.Setting.currentCycle == 0) Remove(effect);
            else
            {
                effect.Setting.ResetDuration();
                OnCycle(effect);
            }
        }
        
        public new void Remove(T effect)
        {
            effect.Reference.StackCount--;
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
        }

        public new virtual void Clear()
        {
            updateEnabled = false;
            App.RemoveListener(UpdateMode.Update, Update);
            base.Clear();
        }
    }
}