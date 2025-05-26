using Game.Scripts.Core;
using UnityEngine;

namespace Game.Core.InteractItems
{
    public abstract class BaseInteract : MonoBehaviour
    {
        [SerializeField]
        protected Collider _interactCollider;

        protected virtual void Awake()
        {
            _interactCollider.gameObject.layer = LayersConfig.InteractableLayerIndex;
        }
    }
}