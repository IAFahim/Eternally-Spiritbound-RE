using _Root.Scripts.Datas.Runtime.Variables;
using Pancake;

namespace _Root.Scripts.Datas.Runtime.Inputs
{
    public abstract class InputConsumer<T> : GameComponent where T : GameComponent
    {
        public abstract Performing<InputProvider<T>> ManualInputProvider { get; }
        public abstract InputProvider<T> AutoInputProvider { get; set; }

        protected virtual void OnEnable()
        {
            if (ManualInputProvider.Active)
            {
                ManualInputProvider.Value.Add(this as T);
            }
        }
        
        protected virtual void OnDisable()
        {
            ManualInputProvider.Value.Remove(this as T);
        }
    }
}