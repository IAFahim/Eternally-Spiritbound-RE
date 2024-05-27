using System.Collections.Generic;
using _Root.Scripts.Datas.Runtime.Movements;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Inputs
{
    public abstract class ManualInput : ScriptableObject
    {
        public List<Move2D> list;
        public abstract void Add(GameObject gameObject);
        public abstract void Remove(GameObject gameObject);
    }
}