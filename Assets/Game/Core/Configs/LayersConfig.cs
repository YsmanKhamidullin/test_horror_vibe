using UnityEngine;

namespace Game.Scripts.Core
{
    public struct LayersConfig
    {
        public static int InteractableLayerIndex => LayerMask.NameToLayer("Interactable");
        public static LayerMask InteractableLayer => LayerMask.GetMask("Interactable");
        public static int EnemyLayerIndex => LayerMask.NameToLayer("Enemy");
        public static LayerMask EnemyLayer => LayerMask.GetMask("Enemy");
    }
}