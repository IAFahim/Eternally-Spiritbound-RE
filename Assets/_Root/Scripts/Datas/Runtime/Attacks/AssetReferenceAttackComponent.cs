using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Root.Scripts.Datas.Runtime.Attacks
{
    [Serializable]
    public class AssetReferenceAttackComponent : AssetReferenceT<AttackComponent>
    {
        /// <summary>
        /// Constructs a new reference to a AttackComponent.
        /// </summary>
        /// <param name="guid">The object guid.</param>
        public AssetReferenceAttackComponent(string guid) : base(guid)
        {
        }
    }
}