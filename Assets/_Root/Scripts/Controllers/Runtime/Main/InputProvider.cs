using Game.Controllers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Controllers.Runtime.Main
{
    public class InputProvider : MonoBehaviour
    {
        public CharacterRef characterRef;

        public bool provideInput = true;
        public Vector2 move;

        private InputActionCollection _inputActionCollection;
        
        private InputAction _moveAction;

        private void OnValidate()
        {
            characterRef ??= GetComponent<CharacterRef>();
        }

        private void Awake()
        {
            _inputActionCollection = new InputActionCollection();
            _moveAction = _inputActionCollection.Player.Move;
        }

        private void OnEnable()
        {
            EnableMove();
        }

        private void OnDisable()
        {
            DisableMove();
        }

        private void EnableMove()
        {
            _moveAction.Enable();
            _moveAction.performed += OnMove;
            _moveAction.canceled += OnMove;
        }

        private void DisableMove()
        {
            _moveAction.Disable();
            _moveAction.performed -= OnMove;
            _moveAction.canceled -= OnMove;
        }

        private void OnMove(InputAction.CallbackContext input)
        {
            move = input.ReadValue<Vector2>();
            if (provideInput) characterRef.main.movement2D.Current = new Vector3(move.x, move.y);
        }
    }
}