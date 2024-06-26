﻿using UnityEngine;

namespace Pancake.Scriptable
{
    [CreateAssetMenu(fileName = "event_vector3.asset", menuName = "Pancake/Scriptable/Events/vector3")]
    [EditorIcon("so_blue_event")]
    public class ScriptableEventVector3 : ScriptableEvent<Vector3>
    {
    }
}