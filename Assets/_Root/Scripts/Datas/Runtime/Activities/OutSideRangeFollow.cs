using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Activities
{
    public class OutSideRangeFollow : RangeFollowComponent
    {
        private bool suspend;
        public override void OnEnable()
        {
            base.OnEnable();
            if (HasAllReference && Condition())
            {
                Direction = (target.Transform.position - Transform.position).normalized;
            }
        }

        public override bool Condition()
        {
            return Vector2.Distance(Transform.position, target.Transform.position) > range;
        }

        protected override void Follow()
        {
            if (Condition())
            {
                Direction = (target.Transform.position - Transform.position).normalized;
                suspend = false;
            }
            else if (!suspend)
            {
                Direction = Vector2.zero;
                suspend = true;
            }
        }
    }
}