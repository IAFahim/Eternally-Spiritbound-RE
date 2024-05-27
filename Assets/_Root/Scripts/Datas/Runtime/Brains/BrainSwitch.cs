using Pancake;
using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Brains
{
    public class BrainSwitch : GameComponent
    {
        [SerializeField] private AIBrain aiBrain;
        [SerializeField] private InputBrain inputBrain;

        public void ActiveAIBrain()
        {
            Destroy(inputBrain);
            aiBrain.gameObject.SetActive(true);
        }

        public void ActiveInputBrain()
        {
            aiBrain.gameObject.SetActive(false);
            inputBrain = gameObject.AddComponent<InputBrain>();
        }
    }
}