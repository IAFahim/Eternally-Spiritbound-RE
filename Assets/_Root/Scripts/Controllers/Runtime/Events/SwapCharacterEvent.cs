using _Root.Scripts.Controllers.Runtime.Characters;
using Pancake;

namespace _Root.Scripts.Controllers.Runtime.Events
{
    public struct SwapCharacterEvent: IEvent
    {
        public Character Character;
    }
}