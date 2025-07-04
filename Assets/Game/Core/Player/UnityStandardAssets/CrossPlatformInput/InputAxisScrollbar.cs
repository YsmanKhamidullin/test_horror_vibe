using UnityEngine;

namespace Game.Core.Player.UnityStandardAssets.CrossPlatformInput
{
	public class InputAxisScrollbar : MonoBehaviour
	{
		public string axis;

		private void Update()
		{
		}

		public void HandleInput(float value)
		{
			CrossPlatformInputManager.SetAxis(axis, value * 2f - 1f);
		}
	}
}
