using Pancake;
using Pancake.Scriptable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Datas.Runtime.Card
{
    public abstract class CardData : GameComponent
    {
        public FloatVariable variable;

        [SerializeField] InputActionReference onClickInputActionReference;

        private InputAction _onClickInputAction;

        protected Vector2 ReadValueScreenPointerPosition() => _onClickInputAction.ReadValue<Vector2>();

        protected virtual void Awake() => _onClickInputAction = onClickInputActionReference.action;

        protected virtual void OnEnable() => _onClickInputAction.Enable();

        protected virtual void OnDisable() => _onClickInputAction.Disable();
    }
}