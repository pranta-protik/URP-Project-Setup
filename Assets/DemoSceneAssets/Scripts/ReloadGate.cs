using Project;
using UnityEngine;

namespace DemoScene
{
	public class ReloadGate : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag(ConstUtils.PLAYER_TAG))
			{
				GameManager.Instance.LevelFailed();
			}
		}
	}
}