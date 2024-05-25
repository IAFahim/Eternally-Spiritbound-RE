using _Root.Scripts.Datas.Runtime.Attacks;
using Pancake.Common;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Attacks
{
    public class DamageOnCollision : AttackComponent
    {
        public override void OnStartup()
        {
            
        }

        protected override void OnUpdate(float deltaTime)
        {
            Debug.Log("DamageOnTouch.OnUpdate");
        }

        protected override void OnComplete()
        {
            Debug.Log("DamageOnTouch.OnComplete");
        }

        protected override void OnCancel()
        {
            Debug.Log("DamageOnTouch.OnCancel");
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("DamageOnTouch.OnCollisionEnter2D");
        }
    }
}