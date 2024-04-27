using _Root.Scripts.Controllers.Runtime.Characters;
using _Root.Scripts.Datas.Runtime.Interfaces;
using _Root.Scripts.Datas.Runtime.Movements;
using Game.Controllers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Controllers.Runtime.Input
{
    public class MovementProvider : InputProvider
    {
        [SerializeField] private Vector2 move;

        private InputActionCollection _inputActionCollection;
        private InputAction _moveAction;
        private IDirection direction;

        protected override void Awake()
        {
            base.Awake();
            _inputActionCollection = new InputActionCollection();
            _moveAction = _inputActionCollection.Player.Move;
        }

        protected override void OnDisable()
        {
            DisableMove();
        }

        public override void SetCharacter(Character character)
        {
            DisableMove();
            direction = character.GetComponent<IDirection>();
            EnableMove();
        }

        public Vector2 Move
        {
            get => move;
            set
            {
                move = value;
                direction.Direction = value;
            }
        }

        private void EnableMove()
        {
            _moveAction.Enable();
            _moveAction.performed += OnMove;
            _moveAction.canceled += OnMoveStop;
        }

        private void DisableMove()
        {
            _moveAction.Disable();
            _moveAction.performed -= OnMove;
            _moveAction.canceled -= OnMoveStop;
        }

        private void OnMove(InputAction.CallbackContext input)
        {
            Move = input.ReadValue<Vector2>();
            direction.IsReceivingMoveInput = true;
        }

        private void OnMoveStop(InputAction.CallbackContext input)
        {
            Move = Vector2.zero;
            direction.IsReceivingMoveInput = false;
        }
    }
}