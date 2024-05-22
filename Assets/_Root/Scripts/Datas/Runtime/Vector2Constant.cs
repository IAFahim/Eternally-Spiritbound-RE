using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime
{
    [EditorIcon("scriptable_const")]
    [CreateAssetMenu(fileName = "const_vector2.asset", menuName = "Pancake/Scriptable/Constants/Vector2")]
    public class Vector2Constant : ScriptableConstant<Vector2>
    {
        public void Set(Vector2 vector2)
        {
            value = vector2;
        }

        public void Reset()
        {
            value = new Vector2(1, 2 / 3f);
        }
    }
}