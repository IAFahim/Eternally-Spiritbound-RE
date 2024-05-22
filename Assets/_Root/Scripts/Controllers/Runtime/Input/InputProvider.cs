using _Root.Scripts.Controllers.Runtime.Characters;
using _Root.Scripts.Controllers.Runtime.Events;
using Pancake;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Controllers.Runtime.Input
{
    public abstract class InputProvider: MonoBehaviour
    {
        [SerializeField] protected InputActionReference inputActionReference;
        
        protected virtual void Awake()
        {   
        }
        
        protected virtual void OnEnable()
        {
        }
        
        protected virtual void OnDisable()
        {
        }

        private void OnSpawnCharacterEventCallBack(SwapCharacterEvent characterEvent)
        {
            SetCharacter(characterEvent.Character);
        }

        public abstract void SetCharacter(Character character);
    }
}