using System;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Variables
{
    [Serializable]
    public class Performing<T>
    {
        [SerializeField] private bool performed;
        [SerializeField] private T value;

        public bool Perfromed
        {
            get => performed;
            set => performed = value;
        }

        public T Value => value;

        public Performing(T value)
        {
            performed = true;
            this.value = value;
        }

        public Performing(bool performed, T value)
        {
            this.performed = performed;
            this.value = value;
        }

        public static implicit operator Performing<T>(T v)
        {
            return new Performing<T>(v);
        }

        public static implicit operator T(Performing<T> o)
        {
            return o.Value;
        }

        public static implicit operator bool(Performing<T> o)
        {
            return o.performed;
        }

        public static bool operator ==(Performing<T> lhs, Performing<T> rhs)
        {
            if (lhs != null && lhs.Value is null) return rhs != null && rhs.Value is null;
            return rhs != null && lhs != null && lhs.Value.Equals(rhs.Value);
        }

        public static bool operator !=(Performing<T> lhs, Performing<T> rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            if (Value is null) return obj is null;
            return Value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}