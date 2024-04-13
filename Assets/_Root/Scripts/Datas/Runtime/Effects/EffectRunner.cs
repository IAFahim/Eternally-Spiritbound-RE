using Pancake;
using Pancake.Scriptable;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public abstract class EffectRunner<T> : ScriptableList<T>
    {
        public bool updateEnabled;

        protected abstract void OnApply(T data);
        protected abstract void Update();
        protected abstract void OnRemove(T data);

        public new virtual void Clear()
        {
            updateEnabled = false;
            App.RemoveListener(UpdateMode.Update, Update);
            base.Clear();
        }

        public new void Add(T data)
        {
            OnApply(data);
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