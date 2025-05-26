using UnityEngine;

namespace Game.Core.Utils
{
    public class RotatorInUpdate : MonoBehaviour
    {
        public float rotationSpeed = 25f;

        private void Update()
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }
}