using _Root.Scripts.Datas.Runtime.Movements;
using _Root.Scripts.Datas.Runtime.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Datas.Runtime.Inputs
{
    public class MoveInputProvider : InputProvider<Move2D>
    {
        public Performing<Vector2> direction;
        public override void ProvideInput(InputAction.CallbackContext context)
        {
            direction = context.ReadValue<Vector2>();
            direction.Active = context.performed;
            ProvideInput(direction);
        }

        protected virtual void ProvideInput(Performing<Vector2> input)
        {
            foreach (var moveInputConsumer in consumerList) moveInputConsumer.Direction = input;
            if (DebugEnabled) Debug.Log($"MoveInputProvider: {input.Value} {input.Active}");
        }
    }
}