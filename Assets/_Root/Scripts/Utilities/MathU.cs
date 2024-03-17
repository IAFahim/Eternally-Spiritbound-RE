namespace _Root.Scripts.Utilities
{
    public static class MathU
    {
        public static float Remap(float x, float A, float B, float C, float D)
        {
            float remappedValue = C + (x-A)/(B-A) * (D - C);
            return remappedValue;
        }
    }
}