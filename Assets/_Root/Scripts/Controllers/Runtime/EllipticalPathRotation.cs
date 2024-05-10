using System;
using _Root.Scripts.Datas.Runtime;
using _Root.Scripts.Datas.Runtime.Interfaces;
using Pancake;
using Pancake.Apex;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime
{
    public class EllipticalPathRotation : GameComponent, IRange, IAngularSpeed, ILoop
    {
        [field: SerializeField] public float Speed { get; set; } = 5;
        [field: SerializeField] public float Radius { get; set; } = 1;
        [field: SerializeField] public int LoopCount { get; set; } = 1;

        public float startAngle;
        public bool rotate;

        public Transform children;
        public Vector2Constant worldScale;

        private float _angle;
        public Vector2 Range => worldScale.Value * Radius;
        private float StartAngleRad => Mathf.Deg2Rad * startAngle;

        void Start()
        {
            _angle = StartAngleRad;
        }

        void Update()
        {
            if (LoopCount < 0 && Mathf.Abs(_angle - StartAngleRad) < .5f) return;

            _angle += Speed * Time.deltaTime;

            if (_angle > Mathf.PI * 2f)
            {
                _angle -= Mathf.PI * 2f;
                LoopCount--;
            }
            Modify();
        }

        private void Modify()
        {
            var (sinAngle, cosAngle) = GetSinCos(_angle);
            if (rotate) Rotate(sinAngle, cosAngle);
            children.localPosition = new Vector3(Range.x * cosAngle, Range.y * sinAngle, 0f);
        }
        

        private (float sinAngle, float cosAngle) GetSinCos(float theta)
        {
            return (Mathf.Sin(theta), Mathf.Cos(theta));
        }

        private void Rotate(float sinAngle, float cosAngle)
        {
            children.rotation = Quaternion.AngleAxis(Mathf.Atan2(sinAngle, cosAngle) * Mathf.Rad2Deg, Vector3.forward);
        }

        [Button]
        private void Reset()
        {
            Start();
            Modify();
        }
    }
}