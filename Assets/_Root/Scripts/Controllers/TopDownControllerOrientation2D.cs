using UnityEngine;

namespace _Root.Scripts.Controllers
{
    [RequireComponent(typeof(TopDownController2D))]
    public class TopDownControllerOrientation2D : MonoBehaviour
    {
        public TopDownController2D topDownController2D;

        private void OnValidate()
        {
            topDownController2D ??= GetComponent<TopDownController2D>();
        }
    }
}