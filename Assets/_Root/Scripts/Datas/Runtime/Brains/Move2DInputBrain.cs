using _Root.Scripts.Datas.Runtime.Lists;
using _Root.Scripts.Datas.Runtime.Movements;

namespace _Root.Scripts.Datas.Runtime.Brains
{
    public class Move2DInputBrain : InputBrain
    {
        public Move2D move2D;
        public Move2DInputConsumers move2DInputConsumers;

        private void Awake() => AttachComponent();
        private void OnEnable() => move2DInputConsumers.Add(move2D);
        private void OnDisable() => move2DInputConsumers.Remove(move2D);
        private void Reset() => AttachComponent();
        private void AttachComponent() => move2D = GetComponent<Move2D>();
    }
}