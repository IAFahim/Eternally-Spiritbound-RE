using System;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    [Serializable]
    public struct ResistanceDatas
    {
        [Range(0, 1)] public float physicalResistance;
        [Range(0, 1)] public float fireResistance;
        [Range(0, 1)] public float waterResistance;
        [Range(0, 1)] public float earthResistance;
        [Range(0, 1)] public float airResistance;
        [Range(0, 1)] public float darkResistance;

        public override string ToString()
        {
            return
                $"Physical: {physicalResistance}, Fire: {fireResistance}, Water: {waterResistance}, Earth: {earthResistance}, Air: {airResistance}, Dark: {darkResistance}";
        }
    }
}