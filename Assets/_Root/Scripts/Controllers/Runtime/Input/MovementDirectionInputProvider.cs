using _Root.Scripts.Datas.Runtime.Lists;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Controllers.Runtime.Input
{
    public class MovementDirectionInputProvider : InputProvider
    {
        [SerializeField] private Vector2 move;
        [SerializeField] private bool isReceivingMoveInput;
        [SerializeReference] public MovementDirectionScriptableList consumerList;

        public Vector2 Move
        {
            get => move;
            set
            {
                if (!provideInput) return;
                move = value;
                foreach (var direction in consumerList)
                {
                    direction.Direction = value;
                    direction.IsReceivingMoveInput = isReceivingMoveInput;
                }
            }
        }

        protected override void OnCanceled(InputAction.CallbackContext context)
        {
            isReceivingMoveInput = false;
            Move = Vector2.zero;
        }

        protected override void OnPerformed(InputAction.CallbackContext context)
        {
            isReceivingMoveInput = true;
            Move = context.ReadValue<Vector2>();
        }
    }
}