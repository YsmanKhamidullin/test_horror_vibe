using UnityEngine;

namespace Game.Core.Npc
{
    public class EntranceDoor : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        public void Open()
        {
            _animator.SetTrigger("Open");
        }

        public void Close()
        {
            _animator.SetTrigger("Close");
        }
    }
}