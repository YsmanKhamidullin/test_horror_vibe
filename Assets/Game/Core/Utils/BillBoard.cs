using UnityEngine;

namespace Game.Core.Utils
{
    public class BillBoard : MonoBehaviour
    {
        private Transform _targetCam;
        private void Start()
        {
            // _targetCam = Project.ProjectContext.PlayerContainer.PlayerController.MainCamera.transform;
            if (Camera.main != null)
            {
                _targetCam = Camera.main.transform;
            }
            else
            {
                Debug.LogError("Can't find camera");
            }
        }

        private void Update()
        {
            var targetPos = _targetCam.position;
            targetPos.y = transform.position.y;
            transform.LookAt(targetPos);
        }
    }
}