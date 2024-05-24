using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Variables
{
    public class NameOrTitleComponent : MonoBehaviour
    {
        [SerializeField] private StringVariable nameOrTitle;
        public StringVariable NameOrTitle => nameOrTitle;
    }
    
}