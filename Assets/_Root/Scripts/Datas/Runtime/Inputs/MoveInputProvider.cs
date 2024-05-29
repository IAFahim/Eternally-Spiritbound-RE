using System.Collections.Generic;
using _Root.Scripts.Datas.Runtime.Movements;
using _Root.Scripts.Datas.Runtime.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Datas.Runtime.Inputs
{
    public class MoveInputProvider : ManualInputProvider
    {
        [SerializeField] private Performing<Vector2> direction;
        public List<Move2D> list;
        public void OnMove(InputAction.CallbackContext context)
        {
            direction = context.ReadValue<Vector2>();
            direction.Perfromed = context.performed;
            Debug.Log(direction.Value);
            foreach (var moveInputConsumer in list)
            {
                moveInputConsumer.Direction = direction;
            }
        }

        public override void AddManualInputTo(GameObject self)
        {
            var move2D = self.GetComponent<Move2D>();
            if (move2D != null)
            {
                list.Add(move2D);
            }
        }

        public override void RemoveManualInputFrom(GameObject self)
        {
            var move2D = self.GetComponent<Move2D>();
            if (move2D != null)
            {
                list.Remove(move2D);
            }
        }
    }
}