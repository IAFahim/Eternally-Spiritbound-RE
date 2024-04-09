using _Root.Scripts.Datas.Runtime.Characters;
using _Root.Scripts.Datas.Runtime.Movements;
using Game.Controllers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Controllers.Runtime.Input
{
    public class InputProvider : MonoBehaviour
    {
        public MainCharacterAuthoring mainCharacterAuthoring;
        public bool provideInput = true;
        [SerializeField] private Vector2 move;

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
            mainCharacterAuthoring.OnRaised += SetCharacter;
            EnableMove();
        }

        private void OnDisable()
        {
            mainCharacterAuthoring.OnRaised -= SetCharacter;
            DisableMove();
        }
        
        private void SetCharacter(Character character)
        {
            movement2DData = character.movement2D;
            provideInput = true;
        }
        
        public Vector2 Move
        {
            get => move;
            set
            {
                move = value;
                if (!provideInput) return;
                movement2DData.Direction = value;
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
            movement2DData.IsMoving = true;
        }

        private void OnMoveStop(InputAction.CallbackContext input)
        {
            Move = Vector2.zero;
            movement2DData.IsMoving = false;
        }
    }
}