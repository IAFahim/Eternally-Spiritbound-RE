using System.Collections.Generic;
using _Root.Scripts.Datas.Runtime.Movements;
using _Root.Scripts.Datas.Runtime.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Root.Scripts.Datas.Runtime.Lists
{
    public class Move2DInputConsumers: ScriptableObject
    {
        public List<Move2D> list;
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
        
        public void Add(Move2D move2D)
        {
            list.Add(move2D);
        }
        
        public void Remove(Move2D move2D)
        {
            list.Remove(move2D);
        }
    }
}