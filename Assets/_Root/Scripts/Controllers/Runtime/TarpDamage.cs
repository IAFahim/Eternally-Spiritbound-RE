using _Root.Scripts.Controllers.Runtime.Statuses;
using _Root.Scripts.Datas.Runtime;
using _Root.Scripts.Datas.Runtime.Statistics;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime
{
    public class TarpDamage : GameComponent
    {
        [Range(0, 1000)] public float damage = 0.1f;
        public DamageType damageType = DamageType.Physical;
        public float invincibilityDuration = 0.1f;

        public float windUpDuration = 0.1f;
        public float activeDuration = 0.1f;
        public float cooldownDuration = 0.5f;
        public int cycles = 1;
        public Collider2D anyCollider2D;
        

        public SpriteRenderer cross;

        private void OnValidate()
        {
            anyCollider2D ??= GetComponent<Collider2D>();
            cross ??= GetComponentInChildren<SpriteRenderer>();
        }

        private void OnEnable()
        {
            anyCollider2D.enabled = false;
            // _sequence = Sequence.Create(cycles)
            //         .Chain(Tween.Color(cross, tweenSettingsWindUp))
            //         .ChainCallback(() =>
            //         {
            //             anyCollider2D.enabled = true;
            //             cross.color = Color.red;
            //             var _ = Tween.LocalPosition(cross.transform, tweenSettingsPositionActive);
            //         })
            //         .ChainDelay(tweenSettingsPositionActive.settings.duration * tweenSettingsPositionActive.settings.cycles)
            //         .ChainCallback(() => { anyCollider2D.enabled = false; })
            //         .Chain(Tween.Color(cross, tweenSettingsCooldown))
            //     ;
        }
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out HealthComponent healthAuthoring))
            {
                healthAuthoring.Damage(damage, Transform.position, damageType, invincibilityDuration);
            }
        }
    }
}