using Pancake;
using Pancake.Scriptable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Controllers.Runtime.Input
{
    public abstract class InputProvider: MonoBehaviour
    {
        public bool provideInput = true;
        [SerializeField] protected InputActionReference inputActionReference;
        private InputAction _inputAction;

        protected virtual void Awake()
        {
            _inputAction = inputActionReference.action;
        }

        protected virtual void OnEnable()
        {
            _inputAction.Enable();
            _inputAction.performed += OnPerformed;
            _inputAction.canceled += OnCanceled;
        }

        protected abstract void OnPerformed(InputAction.CallbackContext context);
        protected abstract void OnCanceled(InputAction.CallbackContext context);

        protected virtual void OnDisable()
        {
            _inputAction.Disable();
            _inputAction.performed -= OnPerformed;
            _inputAction.canceled -= OnCanceled;
        }
    }
}