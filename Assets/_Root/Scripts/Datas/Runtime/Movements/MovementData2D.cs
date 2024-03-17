using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Movements
{
    public class MovementData2D : GameComponent, IGravity
    {
        [field: SerializeReference] public bool GravityActive { get; set; } = true;
        [field: SerializeReference] public float Gravity { get; set; } = 40f;
        [field: SerializeReference] public bool Free { get; set; } = true;
        [field: SerializeReference] public float Friction { get; set; } = 1f;
        [field: SerializeReference] public Vector2 Current { get; set; }
        [field: SerializeReference] public Vector2 Speed { get; set; }

        protected Vector2 impact;
        public Vector2 AddedForce;
    }
}