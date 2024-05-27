using _Root.Scripts.Datas.Runtime.Inputs;
using _Root.Scripts.Datas.Runtime.Movements;
using _Root.Scripts.Datas.Runtime.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Datas.Runtime.Lists
{
    public class Move2DInput : ManualInput
    {
        [SerializeField] private Performing<Vector2> direction;

        public void OnMove(InputAction.CallbackContext context)
        {
            direction = context.ReadValue<Vector2>();
            direction.Perfromed = context.performed;
            foreach (var moveInputConsumer in list)
            {
                moveInputConsumer.Direction = direction;
            }
        }

        public override void Add(GameObject gameObject)
        {
            var move2D = gameObject.GetComponent<Move2D>();
            if (move2D != null)
            {
                list.Add(move2D);
            }
        }

        public override void Remove(GameObject gameObject)
        {
            var move2D = gameObject.GetComponent<Move2D>();
            if (move2D != null)
            {
                list.Remove(move2D);
            }
        }
    }
}