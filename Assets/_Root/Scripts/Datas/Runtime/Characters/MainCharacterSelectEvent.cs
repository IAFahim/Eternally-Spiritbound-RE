using JetBrains.Annotations;
using Pancake.Scriptable;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Root.Scripts.Datas.Runtime.Characters
{
    public class MainCharacterSelectEvent : ScriptableEvent<CharacterData>
    { 
        [SerializeField] private AssetReferenceGameObject mainCharacterPrefabRef;
        [CanBeNull] public CharacterData main;
        
        public new void Raise(CharacterData characterData)
        {
            main = characterData;
            base.Raise(characterData);
        }

        public void Spawn()
        {
            if (main == null)
            {
                mainCharacterPrefabRef.InstantiateAsync().Completed += handle =>
                {
                    main = handle.Result.GetComponent<CharacterData>();
                    main.Transform.SetPositionAndRotation(main.spawnPoint.Value, Quaternion.identity);
                    Raise(main);
                };
            }
        }
    }
}