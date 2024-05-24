using QuickEye.Utility;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.LookUpTables
{
    public class LookUpTable<T, TV> : ScriptableObject
    {
        public TV defaultValue;
        public TV GetOrDefault(T key) => Get(key, out TV sprite) ? sprite : defaultValue;

        [SerializeField] protected UnityDictionary<T, TV> dictionary;

        private void Awake()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }
        
        // public virtual T[] GetKeys() => dictionary.

        public virtual bool Get(T key, out TV value) => dictionary.TryGetValue(key, out value);

        public virtual bool Add(T key, TV value) => dictionary.TryAdd(key, value);

        public virtual bool Replace(T key, TV value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
                return true;
            }

            Add(key, value);
            return false;
        }
        
        public virtual bool Contains(T key) => dictionary.ContainsKey(key);

        public virtual bool Remove(T key) => dictionary.Remove(key);
    }
}