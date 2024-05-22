using _Root.Scripts.Datas.Runtime;
using _Root.Scripts.Datas.Runtime.Interfaces;
using Alchemy.Inspector;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Movements
{
    public class Orbit2D : GameComponent, IRange, IAngularSpeed, IAngleToRotate
    {
        [field: SerializeField] public float Speed { get; set; } = 5;
        [field: SerializeField] public float Radius { get; set; } = 1;
        [field: SerializeField] public float AngleToRotate { get; set; } = 1;

        public float startAngle;

        public Transform children;
        public Vector2Constant worldScale;

        private float _rad, _radRemaining;
        public Vector2 Range => worldScale.Value * Radius;

        void Start()
        {
            _rad = startAngle * Mathf.Deg2Rad;
            _radRemaining = AngleToRotate * Mathf.Deg2Rad;
        }

        void Update()
        {
            if (_radRemaining <= 0) return;
            var thisFrameAngle = Speed * Time.deltaTime;
            _radRemaining -= thisFrameAngle;
            _rad += thisFrameAngle;

            if (_rad > Mathf.PI * 2f)
            {
                _rad -= Mathf.PI * 2f;
            }

            Modify();
        }

        private (float sinValue, float cosValue) GetSinCos(float theta)
        {
            return (Mathf.Sin(theta), Mathf.Cos(theta));
        }

        private void Modify()
        {
            var (sinValue, cosValue) = GetSinCos(_rad);
            Move(Range, sinValue, cosValue);
        }

        protected virtual void Move(Vector2 range, float cosAngle, float sinAngle)
        {
            children.localPosition = new Vector3(range.x * cosAngle, range.y * sinAngle, 0f);
        }

        [Button]
        private void Reset()
        {
            Start();
            Modify();
        }
    }
}