using UnityEngine;

namespace _Root.Scripts.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movements : MonoBehaviour
    {
        public Rigidbody2D rb;
    
        private void OnValidate()
        {
            rb ??= GetComponent<Rigidbody2D>();
        }

        public void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(Vector2.up);
            }else
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(Vector2.down);
            }else
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector2.left);
            }else
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(Vector2.right);
            }else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.1f);
            }
        }
    }
}
