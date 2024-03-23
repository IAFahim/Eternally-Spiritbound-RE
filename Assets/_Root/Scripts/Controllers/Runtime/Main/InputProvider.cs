using _Root.Scripts.Datas.Runtime.Characters;
using Game.Controllers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Controllers.Runtime.Main
{
    public class InputProvider : MonoBehaviour
    {
        public MainCharacterSelectEvent mainCharacterSelectEvent;
        public bool provideInput = true;
        public Vector2 move;

        private InputActionCollection _inputActionCollection;
        private InputAction _moveAction;
        private CharacterData mainCharacter;

        private void Awake()
        {
            _inputActionCollection = new InputActionCollection();
            _moveAction = _inputActionCollection.Player.Move;
        }

        private void OnEnable()
        {
            mainCharacterSelectEvent.OnRaised += SetCharacterSelectEvent;
            EnableMove();
        }

        private void OnDisable()
        {
            mainCharacterSelectEvent.OnRaised -= SetCharacterSelectEvent;
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
            if (provideInput) mainCharacter.movement2D.Direction = new Vector3(move.x, move.y);
        }

        private void SetCharacterSelectEvent(CharacterData character)
        {
            mainCharacter = character;
            provideInput = true;
        }
    }
}