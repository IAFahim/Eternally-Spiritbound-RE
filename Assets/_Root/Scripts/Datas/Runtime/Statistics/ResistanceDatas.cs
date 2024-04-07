using System;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [Serializable]
    public class ResistanceDatas
    {
        [Range(0, 100)] public float physicalResistance;
        [Range(0, 100)] public float fireResistance;
        [Range(0, 100)] public float waterResistance;
        [Range(0, 100)] public float earthResistance;
        [Range(0, 100)] public float airResistance;
        [Range(0, 100)] public float darkResistance;
    }
}