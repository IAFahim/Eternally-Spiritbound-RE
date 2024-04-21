using System;
using System.Collections.Generic;
using System.Linq;
using Pancake;
using QuickEye.Utility;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.LookUpTables
{
    public class SaveAbleLookUpTable<T, TV> : LookUpTable<T, TV>
    {
        public string guid = "b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1";
        
        public virtual void Save()
        {
            (T, TV)[] array = new (T, TV)[dictionary.Count];
            var span = dictionary.ToArray().AsSpan();
            for (int i = 0; i < span.Length; i++)
            {
                array[i] = (span[i].Key, span[i].Value);
            }

            Data.Save(guid, array);
        }
        
        
        public virtual void Load()
        {
            var array = Data.Load<(T, TV)[]>(guid).AsSpan();
            KeyValuePair<T, TV>[] pairs = new KeyValuePair<T, TV>[array.Length];
            for (var i = 0; i < array.Length; i++)
            {
                var pair = array[i];
                pairs[i] = new KeyValuePair<T, TV>(pair.Item1, pair.Item2);
            }

            dictionary = new UnityDictionary<T, TV>(pairs);
        }
    }
}