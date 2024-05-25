using _Root.Scripts.Datas.Runtime.Attacks;
using Cysharp.Threading.Tasks;
using Pancake;
using Pancake.Common;
using UnityEngine;

namespace _Root.Scripts.Controllers.Runtime.Attacks
{
    [CreateAssetMenu(menuName = "Scriptable/Attacks/Melee", fileName = "attack.melee.asset")]
    public class MeleeAttack : Attack
    {
        public async UniTask<GameObject> ExecuteAsync(Transform origin)
        {
            if (!ReferenceAttackComponent.IsDone)
            {
                return await ReferenceAttackComponent.InstantiateAsync();
            }

            return (GameObject)ReferenceAttackComponent.Asset;
        }
    }
}