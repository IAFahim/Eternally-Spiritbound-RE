using _Root.Scripts.Controllers.Runtime.Characters;
using _Root.Scripts.Controllers.Runtime.Events;
using JetBrains.Annotations;
using Pancake;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Root.Scripts.Controllers.Runtime.Main
{
    public class CharacterRef : GameComponent
    { 
        [SerializeField] private AssetReferenceGameObject mainCharacterReferenceGameObject;
        [CanBeNull] public Character main;
        public SwapCharacterEvent onCharacterUpdateEvent;
        public InputProvider inputProvider;

        private void OnValidate()
        {
            inputProvider ??= GetComponent<InputProvider>();
            inputProvider.characterRef = this;
        }

        private void Awake()
        {
            SpawnMainCharacter();
        }

        private void SpawnMainCharacter()
        {
            if (main == null)
            {
                inputProvider.provideInput = false;
                mainCharacterReferenceGameObject.InstantiateAsync().Completed += handle =>
                {
                    main = handle.Result.GetComponent<Character>();
                    main.Transform.SetPositionAndRotation(main.spawnPoint.Value, Quaternion.identity);
                    inputProvider.provideInput = true;
                    onCharacterUpdateEvent.Raise(main);
                };
            }
        }

        private void OnEnable()
        {
            onCharacterUpdateEvent.OnRaised += OnCharacterUpdate;
        }

        private void OnDisable()
        {
            onCharacterUpdateEvent.OnRaised -= OnCharacterUpdate;
        }

        private void OnCharacterUpdate(Character obj)
        {
            main = obj;
        }
    }
}