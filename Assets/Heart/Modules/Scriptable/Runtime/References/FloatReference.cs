namespace Pancake.Scriptable
{
    [System.Serializable]
    public class FloatReference : VariableReference<FloatVariable, float>
    {
        public object OnValueChanged { get; set; }
    }
}