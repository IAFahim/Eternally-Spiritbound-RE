using _Root.Scripts.Datas.Runtime.Variables;

namespace _Root.Scripts.Datas.Runtime.Statistics
{
    public interface ILevel
    {
        Reactive<float> Level { get; set; }
    }
}