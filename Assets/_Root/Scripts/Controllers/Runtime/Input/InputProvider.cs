using _Root.Scripts.Controllers.Runtime.Characters;
using _Root.Scripts.Controllers.Runtime.Events;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Input
{
    public abstract class InputProvider: MonoBehaviour
    {
        private EventBinding<SwapCharacterEvent> _swapCharacterBinding;
        protected virtual void Awake()
        {
            _swapCharacterBinding = new (OnSpawnCharacterEventCallBack);       
        }
        
        protected virtual void OnEnable()
        {
            _swapCharacterBinding.Listen = true;
        }
        
        protected virtual void OnDisable()
        {
            _swapCharacterBinding.Listen = false;
        }

        private void OnSpawnCharacterEventCallBack(SwapCharacterEvent characterEvent)
        {
            SetCharacter(characterEvent.Character);
        }

        public abstract void SetCharacter(Character character);
    }
}