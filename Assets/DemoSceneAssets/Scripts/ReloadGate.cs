using Project;
using UnityEngine;

namespace DemoScene
{
	public class ReloadGate : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag(ConstUtils.PLAYER_TAG))
			{
				GameManager.Instance.LevelFailed();
			}
		}
	}
}