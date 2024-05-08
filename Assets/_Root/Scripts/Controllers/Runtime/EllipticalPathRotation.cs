using Pancake;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime
{
    public class EllipticalPathRotation : GameComponent
    {
        public Vector2 center;
        public Vector2 radius;
        public float startAngle;
        public float angularSpeed;
        public bool rotate;

        [SerializeField] private float angle;

        void Start()
        {
            angle = startAngle;
            center = Transform.position;
        }

        void Update()
        {
            angle += angularSpeed * Time.deltaTime;
            if (angle > Mathf.PI * 2f) angle -= Mathf.PI * 2f;
            var (sinAngle, cosAngle) = GetSinCos(angle);
            if (rotate) Rotate(sinAngle, cosAngle);
            transform.localPosition = new Vector3(center.x + radius.x * cosAngle, center.y + radius.y * sinAngle, 0f);
        }

        private (float sinAngle, float cosAngle) GetSinCos(float theta)
        {
            return (Mathf.Sin(theta), Mathf.Cos(theta));
        }

        private void Rotate(float sinAngle, float cosAngle)
        {
            Transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(sinAngle, cosAngle) * Mathf.Rad2Deg, Vector3.forward);
        }
    }
}