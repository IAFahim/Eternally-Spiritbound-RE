﻿using _Root.Scripts.Datas.Runtime.Characters;
using Pancake;
using UnityEngine.Serialization;

namespace _Root.Scripts.Controllers.Runtime.Characters
{
    public class MainCharacterSpawner : GameComponent
    {
        public MainCharacterSelectEvent mainCharacterSelectEvent;

        private void OnEnable()
        {
            mainCharacterSelectEvent.Spawn();
        }
    }
}