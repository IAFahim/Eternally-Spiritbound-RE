using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Controllers.Runtime.Main
{
    public class InputProvider : MonoBehaviour
    {
        public MainCharacter mainCharacter;
        public bool provideInput;
        public Vector2 move;

        private InputActionCollection _inputActionCollection;
        private InputAction _moveAction;

        private void OnValidate()
        {
            mainCharacter ??= GetComponent<MainCharacter>();
        }

        private void Awake()
        {
            _inputActionCollection = new InputActionCollection();
            _moveAction = _inputActionCollection.Player.Move;
        }

        private void OnEnable()
        {
            _moveAction.Enable();
            _moveAction.performed += OnMove;
            _moveAction.canceled += OnMove;
        }

        private void OnMove(InputAction.CallbackContext obj)
        {
            move = obj.ReadValue<Vector2>();
            if (provideInput) mainCharacter.Movement2D.Current = new Vector3(move.x, move.y);
        }

        private void OnDisable()
        {
            _moveAction.Disable();
            _moveAction.performed -= OnMove;
        }
    }
}