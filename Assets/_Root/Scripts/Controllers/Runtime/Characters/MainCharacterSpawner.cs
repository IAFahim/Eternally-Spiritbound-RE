using _Root.Scripts.Datas.Runtime.Characters;
using Pancake;
using UnityEngine.Serialization;

namespace _Root.Scripts.Controllers.Runtime.Characters
{
    public class MainCharacterSpawner : GameComponent
    {
        public MainCharacterAuthoring mainCharacterAuthoring;

        private void OnEnable()
        {
            mainCharacterAuthoring.Spawn();
        }
    }
}