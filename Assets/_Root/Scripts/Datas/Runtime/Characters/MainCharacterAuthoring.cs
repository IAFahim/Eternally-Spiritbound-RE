using JetBrains.Annotations;
using Pancake.Scriptable;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Root.Scripts.Datas.Runtime.Characters
{
    public class MainCharacterAuthoring : ScriptableEvent<Character>
    { 
        [SerializeField] private AssetReferenceGameObject mainCharacterPrefabRef;
        [CanBeNull] public Character spawnedMainCharacter;
        
        public new void Raise(Character characterData)
        {
            spawnedMainCharacter = characterData;
            base.Raise(characterData);
        }

        public void Spawn()
        {
            if (spawnedMainCharacter == null)
            {
                mainCharacterPrefabRef.InstantiateAsync().Completed += handle =>
                {
                    spawnedMainCharacter = handle.Result.GetComponent<Character>();
                    spawnedMainCharacter!.Transform.SetPositionAndRotation(spawnedMainCharacter.spawnPoint.Value, Quaternion.identity);
                    Raise(spawnedMainCharacter);
                };
            }
        }
    }
}