﻿using System;
using Pancake.Apex;
using QuickEye.Utility;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.LookUpTables
{
    [UseDefaultEditor]
    [Serializable]
    public class LookUpTable<T, TV> : ScriptableObject
    {
        [SerializeField] protected UnityDictionary<T, TV> dictionary;
        
        public TV this[T key] => dictionary[key];

        private void Awake()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }

        public bool Get(T key, out TV value) => dictionary.TryGetValue(key, out value);

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

        public virtual bool Remove(T key) => dictionary.Remove(key);
    }
}