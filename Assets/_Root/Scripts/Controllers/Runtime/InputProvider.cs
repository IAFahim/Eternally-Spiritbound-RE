using _Root.Scripts.Controllers.Runtime.TopDowns.Bases;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Controllers.Runtime
{
    public class InputProvider : MonoBehaviour
    {
        public TopDownController mainPlayerTopDownController;
        
        private InputActionCollection inputActionCollection;
        private InputAction moveAction;

        public Vector2 move;
        
        private void Awake()
        {
            inputActionCollection = new InputActionCollection();
            moveAction = inputActionCollection.Player.Move;
        }

        private void OnEnable()
        {
            moveAction.Enable();
            moveAction.performed += OnMove;
            moveAction.canceled += OnMove;
        }

        private void OnMove(InputAction.CallbackContext obj)
        {
            move = obj.ReadValue<Vector2>();
            mainPlayerTopDownController.CurrentMovement = new Vector3(move.x, move.y);
        }

        private void OnDisable()
        {
            moveAction.Disable();
            moveAction.performed -= OnMove;
        }
    }
}
