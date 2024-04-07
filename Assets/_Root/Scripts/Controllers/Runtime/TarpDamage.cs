using _Root.Scripts.Datas.Runtime;
using _Root.Scripts.Datas.Runtime.Statistics;
using Pancake;
using PrimeTween;
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

        private Sequence _sequence;
        public TweenSettings<Color> tweenSettingsWindUp;
        public TweenSettings<Vector3> tweenSettingsPositionActive;
        public TweenSettings<Color> tweenSettingsCooldown;

        public SpriteRenderer cross;

        private void OnValidate()
        {
            anyCollider2D ??= GetComponent<Collider2D>();
            cross ??= GetComponentInChildren<SpriteRenderer>();
            windUpDuration = tweenSettingsWindUp.settings.duration * tweenSettingsWindUp.settings.cycles;
            activeDuration = tweenSettingsPositionActive.settings.duration *
                             tweenSettingsPositionActive.settings.cycles;
            cooldownDuration = tweenSettingsCooldown.settings.duration * tweenSettingsCooldown.settings.cycles;
        }
        
        

        private void OnEnable()
        {
            anyCollider2D.enabled = false;
            if (_sequence.isAlive) _sequence.Complete();
            _sequence = Sequence.Create(cycles)
                    .Chain(Tween.Color(cross, tweenSettingsWindUp))
                    .ChainCallback(() =>
                    {
                        anyCollider2D.enabled = true;
                        cross.color = Color.red;
                    })
                    .Chain(Tween.LocalPosition(cross.transform, tweenSettingsPositionActive))
                    .ChainCallback(() => { anyCollider2D.enabled = false; })
                    .Chain(Tween.Color(cross, tweenSettingsCooldown))
                ;
        }
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out HealthAuthoring healthAuthoring))
            {
                healthAuthoring.Damage(damage, Transform.position, damageType, invincibilityDuration);
            }
        }
    }
}