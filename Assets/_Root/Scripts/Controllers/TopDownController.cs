using _Root.Scripts.Datas;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Controllers
{
    public class TopDownController : GameComponent, IGravity
    {
        [field: SerializeReference] public bool GravityActive { get; set; } = true;
        [field: SerializeReference] public float Gravity { get; set; } = 40f;

        [field: SerializeReference] public bool FreeMovement { get; set; } = true;
        [field: SerializeReference] public float Friction { get; set; } = 1f;
        [field: SerializeReference] public Vector3 CurrentMovement { get; set; }
        [field: SerializeReference] public Vector3 Speed { get; set; }
        public virtual bool OnAMovingPlatform { get; set; }
    }
}