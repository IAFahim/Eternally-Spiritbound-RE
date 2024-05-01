using _Root.Scripts.Controllers.Runtime.Cards;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Root.Scripts.Presentations.Runtime.Ui
{
    public class CardUI : CardControl
    {
        private Tween _scaleTween;
        public TweenSettings<Vector3> scaleTweenSettings;
        private Vector2 _startScale, _endScale;


        protected override void OnEnable()
        {
            base.OnEnable();
            SnapShotTweenSettings();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            OnBeginDragVisual();
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            OnEndDragVisual();
        }

        private void OnBeginDragVisual()
        {
            if (_scaleTween.isAlive) _scaleTween.Complete();
            SetStartScale();
            _scaleTween = Tween.Scale(Transform, scaleTweenSettings);
        }


        private void OnEndDragVisual()
        {
            if (_scaleTween.isAlive) _scaleTween.Complete();
            SetEndScale();
            _scaleTween = Tween.Scale(Transform, scaleTweenSettings);
        }


        private void SnapShotTweenSettings()
        {
            (scaleTweenSettings.startValue, scaleTweenSettings.endValue) = (_startScale, _endScale);
        }

        private void SetStartScale() =>
            (scaleTweenSettings.startValue, scaleTweenSettings.endValue) = (_startScale, _endScale);

        private void SetEndScale() =>
            (scaleTweenSettings.startValue, scaleTweenSettings.endValue) = (_endScale, _startScale);

        protected override void OnDisable()
        {
            base.OnDisable();
            if (_scaleTween.isAlive) _scaleTween.Complete();
        }
    }
}