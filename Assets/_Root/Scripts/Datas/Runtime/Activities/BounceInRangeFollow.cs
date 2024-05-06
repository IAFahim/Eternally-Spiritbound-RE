using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Activities
{
    public class BounceInRangeFollow : RangeFollowComponent
    {
        public override void OnEnable()
        {
            base.OnEnable();
            if (HasAllReference && Condition())
            {
                Direction = Random.insideUnitCircle.normalized;
            }
        }

        public override bool Condition()
        {
            return Vector2.Distance(Transform.position, target.Transform.position) < range;
        }

        protected override void Follow()
        {
            if (Condition()) return;
            Direction = (target.Transform.position - Transform.position).normalized;
        }

        private void OnDrawGizmos()
        {
            if (DebugEnabled)
            {
                if (HasAllReference)
                {
                    Gizmos.color = Condition() ? Color.green : Color.red;
                    var targetPosition = Target.Transform.position;
                    Gizmos.DrawWireSphere(targetPosition, range);
                }

                Gizmos.color = Color.white;
                var transformPosition = Transform.position;
                Gizmos.DrawLine(transformPosition, transformPosition + (Vector3)Direction);
            }
        }
    }
}