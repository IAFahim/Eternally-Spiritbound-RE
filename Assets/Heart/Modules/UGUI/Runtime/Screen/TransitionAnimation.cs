﻿using System;
using System.Text.RegularExpressions;
using Alchemy.Inspector;
using UnityEngine;

namespace Pancake.UI
{
    [Serializable]
    public sealed class TransitionAnimation
    {
        [SerializeField] private string partnerIdRegex;
        [SerializeField] private AnimationAssetType type;

#if UNITY_EDITOR
        private bool IsMonobehaviour => type == AnimationAssetType.MonoBehaviour;
        private bool IsScriptableObject => type == AnimationAssetType.MonoBehaviour;
#endif

        [SerializeField, ShowIf("IsMonobehaviour"), LabelText("Animation")]
        private UITransitionComponent uiTransitionComponent;

        [SerializeField, ShowIf("IsScriptableObject"), LabelText("Animation")]
        private UITransitionAnimationSO uiTransitionAnimationSo;

        private Regex _partnerIdRegexCache;

        public bool IsValid(string partnerId)
        {
            if (GetAnimation() == null) return false;

            // If the partner identifier is not registered, the animation is always valid.
            if (string.IsNullOrEmpty(partnerIdRegex)) return true;

            if (string.IsNullOrEmpty(partnerId)) return false;

            if (_partnerIdRegexCache == null) _partnerIdRegexCache = new Regex(partnerIdRegex);

            return _partnerIdRegexCache.IsMatch(partnerId);
        }

        public ITransitionAnimation GetAnimation()
        {
            return type switch
            {
                AnimationAssetType.MonoBehaviour => uiTransitionComponent,
                AnimationAssetType.ScriptableObject => UnityEngine.Object.Instantiate(uiTransitionAnimationSo),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}