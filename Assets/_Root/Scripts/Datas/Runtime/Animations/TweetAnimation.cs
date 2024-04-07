using PrimeTween;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Animations
{
    public static class TweetAnimation
    {
        public static Sequence Trap3()
        {
            // Sequence.Create(cycles, CycleMode.Yoyo)
            //     .Group(Tween.Color(cross, tweenSettingsWindUp))
            //     .ChainCallback(() => { anyCollider2D.enabled = true; })
            //     .Group(Tween.Color(cross, tweenSettingsActive))
            //     .ChainCallback(() => { anyCollider2D.enabled = false; })
            return Sequence.Create();
        }
    }
}