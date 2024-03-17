using System;
using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [Serializable]
    public class Health : GameComponent, IHealth, IInvulnerable, IDeath
    {
        #region IHealth

        [Tooltip("the current health of the character")]
        [field: SerializeReference]
        public float CurrentHealth { get; private set; } = 100;
        
        [Tooltip("the maximum health of the character")]
        [field: SerializeReference]
        public float MaxHealth { get; private set; } = 100;
        
        public bool increaseHealthWithMaxHealth;
        
        public float SetHealth(float value)
        {
            return CurrentHealth = Mathf.Clamp(value, 0, MaxHealth);
        }
        
        public float SetMaxHealth(float value)
        {
            float newMax = Mathf.Max(value, 0);
            if(increaseHealthWithMaxHealth) SetHealth(CurrentHealth + newMax - MaxHealth);
            return MaxHealth = Mathf.Max(value, 0);
        }
        
        #endregion

        [Tooltip("If this is true, this object can't take damage at this time")]
        [field: SerializeReference]
        public bool Invulnerable { get; set; }


        [Tooltip("if this is true, the character is dead")]
        [field: SerializeReference]
        public bool IsDead { get; set; }

        [Tooltip("if this is true, the model will be disabled instantly on death (if a model has been set)")]
        [field: SerializeReference]
        public bool DisableOnModeDeath { get; set; }

        private void OnEnable()
        {
            
        }
    }
}