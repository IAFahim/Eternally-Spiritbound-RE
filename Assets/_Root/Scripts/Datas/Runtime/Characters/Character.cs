using _Root.Scripts.Datas.Runtime.Movements;
using _Root.Scripts.Datas.Runtime.Statistics;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Root.Scripts.Datas.Runtime.Characters
{
    [RequireComponent(typeof(Movement2DData))]
    public class Character : GameComponent
    {
        public int id;
        public StringReference characterName = new()
        {
            useLocal = true,
            localValue = "Character",
        };

        public CharacterType characterType;
        public Stats stats;
        public Vector2Variable spawnPoint;
        public Movement2DData movement2D;
        public HealthAuthoring healthAuthoring;
        
        public GameObject model;
        public MainCharacterAuthoring mainCharacterAuthoring;

        private void OnValidate()
        {
            stats ??= GetComponent<Stats>();
            healthAuthoring ??= GetComponent<HealthAuthoring>();
            movement2D ??= GetComponent<Movement2DData>();
        }
    }
}