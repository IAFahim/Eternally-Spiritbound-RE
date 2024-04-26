using _Root.Scripts.Datas.Runtime.Interfaces;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Movements
{
    public class Movement2DData : GameComponent, IGravity, IDirection, IFriction
    {
        [field: SerializeReference] public bool GravityActive { get; set; } = true;
        [field: SerializeReference] public float Gravity { get; set; } = 40f;
        [field: SerializeReference] public bool Free { get; set; } = true;
        [field: SerializeReference] public float Friction { get; set; } = 1f;
        [field: SerializeReference] public Vector2 Direction { get; set; }
        [field: SerializeReference] public Vector2 Speed { get; set; } = Vector2.one;
        [field: SerializeReference] public bool IsReceivingMoveInput { get; set; }

        protected Vector2 impact;
        public Vector2 AddedForce;
    }
}