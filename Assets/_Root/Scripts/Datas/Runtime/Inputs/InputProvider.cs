using System.Collections.Generic;
using _Root.Scripts.Datas.Runtime.Interfaces;
using Pancake;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Datas.Runtime.Inputs
{
    public abstract class InputProvider<T> : ScriptableObject, IDebug where T : GameComponent
    {
        [SerializeField] private bool debugEnabled;
        public List<T> consumerList;

        public void Add(T consumer)
        {
            consumerList.Add(consumer);
        }

        public void Remove(T consumer)
        {
            consumerList.Remove(consumer);
        }

        public abstract void ProvideInput(InputAction.CallbackContext context);

        public bool DebugEnabled
        {
            get => debugEnabled;
            set => debugEnabled = value;
        }
    }
}