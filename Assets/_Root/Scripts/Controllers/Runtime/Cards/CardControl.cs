using _Root.Scripts.Datas.Runtime.Card;
using Pancake;
using Pancake.Apex;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Root.Scripts.Controllers.Runtime.Cards
{
    public class CardControl : CardData, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        
        [DisableInEditorMode] public bool dragging;
        [SerializeField] private float moveSpeedLimit = 50;
        [SerializeField] private Canvas canvas;
        [SerializeField] private GraphicRaycaster graphicRayCaster;
        [SerializeField] private Image image;

        private Vector3 _offset;
        private Camera _mainCamera;
        private Vector2 screenBounds;
        
        protected override void Awake()
        {
            base.Awake();
            _mainCamera = Camera.main;
            AttachComponentIfMissing();
        }

        public void OnDrag(PointerEventData _)
        {
        }

        private void Update()
        {
            if (dragging)
            {
                ClampPositionInsideScreen();
                SmoothlyTranslate();
            }
        }


        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            UpdateScreenBound();
            SetRayCastTarget(false);
            dragging = true;
            canvas.overrideSorting = true;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            SetRayCastTarget(true);
            dragging = false;
        }

        private void SetRayCastTarget(bool value)
        {
            graphicRayCaster.enabled = value;
            image.raycastTarget = value;
        }

        void ClampPositionInsideScreen()
        {
            Vector3 clampedPosition = Transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x, screenBounds.x);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y, screenBounds.y);
            Transform.position = new Vector3(clampedPosition.x, clampedPosition.y, 0);
        }

        void SmoothlyTranslate()
        {
            Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(ReadValueScreenPointerPosition()) - _offset;
            Vector2 position = Transform.position;
            Vector2 direction = (worldPosition - position).normalized;
            var pointerMoveSpeed = Vector2.Distance(position, worldPosition) / Time.deltaTime;
            Vector2 velocity = direction * moveSpeedLimit.Min(pointerMoveSpeed);
            Transform.Translate(velocity * Time.deltaTime);
        }

        private void UpdateScreenBound()
        {
            screenBounds = _mainCamera.ScreenToWorldPoint(
                new Vector3(Screen.width, Screen.height, _mainCamera.transform.position.z)
            );
        }

        private void Reset()
        {
            AttachComponentIfMissing();
        }
        
        private void AttachComponentIfMissing()
        {
            canvas ??= GetComponentInParent<Canvas>();
            graphicRayCaster ??= GetComponentInParent<GraphicRaycaster>();
            image ??= GetComponent<Image>();
        }
    }
}