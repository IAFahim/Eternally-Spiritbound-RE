using UnityEngine;

namespace _Root.Scripts.Utilities
{
    public static class FloatExt
    {
        public static bool IsEmpty(this float value)
        {
            return Mathf.Approximately(value, 0);
        }
        
        public static float IncreasePercentage(this float value, float percentage)
        {
            return (1 + percentage) * value;
        }
        
        public static float DecreasePercentage(this float value, float percentage)
        {
            return (1 - percentage) * value;
        }
        

        public static float IncreasePercentageNormalized(this float value, float percentage)
        {
            return Mathf.Clamp(value.IncreasePercentage(percentage), 0, 1);
        }
        
        public static float MapRange(this float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
        }
        
        public static float RoundToDecimal(this float value, int decimalPlaces)
        {
            float multiplier = Mathf.Pow(10, decimalPlaces);
            return Mathf.Round(value * multiplier) / multiplier;
        }
    }
}