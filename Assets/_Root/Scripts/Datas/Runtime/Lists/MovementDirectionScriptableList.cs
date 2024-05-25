using _Root.Scripts.Datas.Runtime.Interfaces;
using Pancake;
using Pancake.Scriptable;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Lists
{
    [CreateAssetMenu(fileName = "list_vector2.asset", menuName = "Scriptable/Lists/MovementDirection")]
    [EditorIcon("so_blue_list")]
    public class MovementDirectionScriptableList: ScriptableList<IMovementDirection>
    {
    }
}