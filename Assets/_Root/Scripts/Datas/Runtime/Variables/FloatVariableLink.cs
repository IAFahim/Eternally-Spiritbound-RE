using System;
using System.Collections.Generic;
using _Root.Scripts.Datas.Runtime.Variables._Root.Scripts.Datas.Runtime.Linker;
using Pancake.Apex;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Variables
{
    [CreateAssetMenu(fileName = "FloatVariableLink", menuName = "Scriptable/Variable/Link/FloatVariableLink")]
    public class FloatVariableLink : ScriptableObject
    {
        [Array] [SerializeField] private List<FloatLink> floatLinks;
        private Dictionary<FloatReference, FloatReference> _stats;

        private void OnEnable()
        {
            RegenerateDictionary();
        }

        [Button]
        public void RegenerateDictionary()
        {
            _stats = new Dictionary<FloatReference, FloatReference>();
            foreach (var floatLink in floatLinks)
            {
                _stats.Add(floatLink.key, floatLink.value);
            }
        }

        public bool TryGetValue(FloatReference key, out FloatReference value) => _stats.TryGetValue(key, out value);

        public void Add(FloatReference key, FloatReference value)
        {
            _stats.Add(key, value);
        }

        public bool Remove(FloatReference key) => _stats.Remove(key);
    }


    namespace _Root.Scripts.Datas.Runtime.Linker
    {
        [Serializable]
        public class FloatLink
        {
            public FloatReference key;
            public FloatReference value;
        }
    }
}