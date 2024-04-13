using UnityEngine;

namespace _Root.Scripts.Datas.Runtime.Effects
{
    public class FrictionSideEffect : SideEffect<FrictionSettings>
    {
        protected override void Update()
        {
            if (updateEnabled == false) return;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                FrictionSettings frictionSettings = list[i];
                if (!frictionSettings.Tick(Time.deltaTime))
                {
                    list.RemoveAt(i);
                    Remove(frictionSettings);
                }
            }
        }
    }
}