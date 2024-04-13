using Pancake;
using Pancake.Scriptable;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public abstract class SideEffect<T> : ScriptableList<T>
    {
        public bool updateEnabled;

        public new virtual void Clear()
        {
            updateEnabled = false;
            App.RemoveListener(UpdateMode.Update, Update);
            base.Clear();
        }

        public new virtual void Add(T settings)
        {
            if (Count == 0)
            {
                App.AddListener(UpdateMode.Update, Update);
                updateEnabled = true;
            }

            base.Add(settings);
        }

        protected abstract void Update();

        public new virtual bool Remove(T settings)
        {
            var isPresent = base.Remove(settings);
            if (Count == 0)
            {
                App.RemoveListener(UpdateMode.Update, Update);
                updateEnabled = false;
            }

            return isPresent;
        }
    }
}