using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.Canvas
{
    public class CanvasWindow : BaseWindow
    {
        [SerializeField]
        private GraphicRaycaster _graphicRaycaster;

        public void DisableRaycast()
        {
            _graphicRaycaster.enabled = false;
        }

        public void EnableRaycast()
        {
            _graphicRaycaster.enabled = true;
        }
    }
}