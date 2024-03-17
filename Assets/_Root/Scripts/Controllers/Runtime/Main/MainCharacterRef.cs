﻿using _Root.Scripts.Controllers.Runtime.Characters.Base;
using _Root.Scripts.Controllers.Runtime.Events;
using _Root.Scripts.Controllers.Runtime.Movements;
using _Root.Scripts.Datas.Runtime.Statistics;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Main
{
    public class MainCharacterRef: MonoBehaviour
    {
        public SwapCharacterEvent swapCharacterEvent;
        public Character character;
        public InputProvider inputProvider;

        public Movement2D Movement2D=> character.movement2D;
        public Health Health => character.health;
        private void OnValidate()
        {
            inputProvider ??= GetComponent<InputProvider>();
        }

        private void OnEnable()
        {
            swapCharacterEvent.OnRaised += OnSwapCharacter;
        }
        
        private void OnDisable()
        {
            swapCharacterEvent.OnRaised -= OnSwapCharacter;
        }
        
        private void OnSwapCharacter(Character obj)
        {
            character = obj;
        }
    }
}