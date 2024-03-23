using _Root.Scripts.Datas.Runtime.Characters;
using _Root.Scripts.Datas.Runtime.Movements;
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
        [SerializeField] private Movement2DData movement2DData;

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
            move = input.ReadValue<Vector2>();
            if (!provideInput) return;
            movement2DData.Direction = new Vector3(move.x, move.y);
            movement2DData.IsMoving = true;
        }

        private void OnMoveStop(InputAction.CallbackContext input)
        {
            if (!provideInput) return;
            movement2DData.Direction = Vector3.zero;
            movement2DData.IsMoving = false;
        }

        private void SetCharacterSelectEvent(CharacterData character)
        {
            movement2DData = character.movement2D;
            provideInput = true;
        }
    }
}