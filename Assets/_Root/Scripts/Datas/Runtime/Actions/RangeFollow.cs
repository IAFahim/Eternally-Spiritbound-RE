using System;
using _Root.Scripts.Datas.Runtime.Movements;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Actions
{
    public class RangeFollow: Follow
    {
        public float range;
        public Movement2DData movement2D;
        
        private void OnValidate()
        {
            movement2D ??= GetComponent<Movement2DData>();
        }
        public void Update()
        {
            if (target == null) return;
            var targetPosition = target.Transform.position;
            var distance = Vector2.Distance(Transform.position, targetPosition);
            if (distance > range) return;
            var direction = (targetPosition - Transform.position).normalized;
            movement2D.Direction = direction;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Transform.position, range);
        }
    }
}