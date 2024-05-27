using _Root.Scripts.Datas.Runtime;
using _Root.Scripts.Datas.Runtime.Interfaces;
using Alchemy.Inspector;
using Pancake;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Root.Scripts.Controllers.Runtime.Movements.Physics
{
    public sealed class Orbit2Drb : GameComponent, IRange, IAngularSpeed, IAngleToRotate
    {
        [field: SerializeField] public float Speed { get; set; } = 5;
        public Vector2 SpeedVector => Speed * worldScale.Value;
        [field: SerializeField] public float Radius { get; set; } = 1;
        [field: SerializeField] public float AngleToRotate { get; set; } = 1;

        public float startAngle;

        [FormerlySerializedAs("rigidbody")] public Rigidbody2D rb;
        public Vector2Constant worldScale;

        private float _rad, _radRemaining;
        public Vector2 Range => worldScale.Value * Radius;
        public Transform model;

        void Start()
        {
            _rad = startAngle * Mathf.Deg2Rad;
            _radRemaining = AngleToRotate * Mathf.Deg2Rad;
        }

        void FixedUpdate()
        {
            if (_radRemaining <= 0) return;
            var thisFrameAngle = Speed * Time.fixedDeltaTime;
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

        private void Move(Vector2 range, float cosAngle, float sinAngle)
        {
            var ellipticPos = new Vector2(range.x * cosAngle, range.y * sinAngle) + (Vector2)model.position;
            rb.MovePosition(ellipticPos);
        }

        [Button]
        private void Reset()
        {
            Start();
            Modify();
        }
    }
}