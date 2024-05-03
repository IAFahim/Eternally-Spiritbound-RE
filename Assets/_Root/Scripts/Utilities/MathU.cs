namespace _Root.Scripts.Utilities
{
    public static class MathU
    {
        public static double Remap(double value, double fromMin, double fromMax, double toMin, double toMax)
        {
            return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
        }
    }
}