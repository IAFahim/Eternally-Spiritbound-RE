using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Root.Scripts.Controllers.Runtime.Characters
{
    public sealed class MainCharacterSpawner : MonoBehaviour
    {
        [SerializeField] private AssetReferenceGameObject assetReferenceGameObject;
        public Character spawnedMainCharacter;

        private void OnEnable()
        {
            SpawnAndSet();
        }

        private void SpawnAndSet()
        {
            if (!spawnedMainCharacter)
            {
                assetReferenceGameObject.InstantiateAsync().Completed += handle =>
                {
                    spawnedMainCharacter = handle.Result.GetComponent<Character>();
                    spawnedMainCharacter!.Transform.SetPositionAndRotation(
                        spawnedMainCharacter.spawnPoint.Value, Quaternion.identity
                    );
                };
            }
        }
    }
}