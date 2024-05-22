﻿using System.Collections;
using Pancake.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Pancake.UI
{
    public class PopupBackdrop : GameComponent
    {
        public PopupBackdropTransitionContainer animationContainer;
        [SerializeField] private bool closePopupWhenClicked;

        private CanvasGroup _canvasGroup;
        private RectTransform _parentTransform;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = (RectTransform) transform;
            _canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
            if (closePopupWhenClicked)
            {
                if (!TryGetComponent<Image>(out var image))
                {
                    image = gameObject.AddComponent<Image>();
                    image.color = Color.clear;
                }

                if (!TryGetComponent<Button>(out var button))
                {
                    button = gameObject.AddComponent<Button>();
                    button.transition = Selectable.Transition.None;
                }

                button.onClick.AddListener(() =>
                {
                    var popupContainer = PopupContainer.Of(transform);
                    if (popupContainer.IsInTransition) return;
                    popupContainer.Pop(true);
                });
            }
        }

        public void Setup(RectTransform parent)
        {
            _parentTransform = parent;
            _rectTransform.FillWithParent(_parentTransform);
            _canvasGroup.interactable = closePopupWhenClicked;
            gameObject.SetActive(false);
        }

        internal AsyncProcessHandle Enter(bool playAnimation) { return App.StartCoroutine(EnterRoutine(playAnimation)); }

        private IEnumerator EnterRoutine(bool playAnimation)
        {
            gameObject.SetActive(true);
            _rectTransform.FillWithParent(_parentTransform);
            _canvasGroup.alpha = 1;

            if (playAnimation)
            {
                var anim = animationContainer.GetAnimation(true);
#if PANCAKE_LITMOTION
                if (anim == null) anim = DefaultTransitionSetting.PopupBackdropEnter;
#endif

                if (anim.Duration > 0)
                {
                    anim.Setup(_rectTransform);
                    yield return App.StartCoroutine(anim.CreateRoutine());
                }
            }

            _rectTransform.FillWithParent(_parentTransform);
        }

        internal AsyncProcessHandle Exit(bool playAnimation) { return App.StartCoroutine(ExitRoutine(playAnimation)); }

        private IEnumerator ExitRoutine(bool playAnimation)
        {
            gameObject.SetActive(true);
            _rectTransform.FillWithParent(_parentTransform);
            _canvasGroup.alpha = 1;

            if (playAnimation)
            {
                var anim = animationContainer.GetAnimation(false);
#if PANCAKE_LITMOTION
                if (anim == null) anim = DefaultTransitionSetting.PopupBackdropExit;
#endif

                if (anim.Duration > 0)
                {
                    anim.Setup(_rectTransform);
                    yield return App.StartCoroutine(anim.CreateRoutine());
                }
            }

            _canvasGroup.alpha = 0;
            gameObject.SetActive(false);
        }
    }
}