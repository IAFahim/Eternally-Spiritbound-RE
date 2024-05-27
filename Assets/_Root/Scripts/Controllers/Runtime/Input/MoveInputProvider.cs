using _Root.Scripts.Datas.Runtime.Lists;
using _Root.Scripts.Datas.Runtime.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Controllers.Runtime.Input
{
    public class MoveInputProvider : MonoBehaviour
    {
        [SerializeField] private Performing<Vector2> direction;
        public MoveInputConsumerScriptableList moveInputConsumers;
        
        public void OnMove(InputAction.CallbackContext context)
        {
            direction = context.ReadValue<Vector2>();
            direction.Perfromed = context.performed;
            foreach (var moveInputConsumer in moveInputConsumers)
            {
                moveInputConsumer.Direction = direction;
            }
        }
    }
}