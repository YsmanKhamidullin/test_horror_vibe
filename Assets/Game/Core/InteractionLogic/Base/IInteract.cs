using Cysharp.Threading.Tasks;
using Game.Core.Player;

namespace Game.Core.InteractItems
{
    public interface IInteract
    {
        public UniTask Interact(PlayerController playerController);
    }
}