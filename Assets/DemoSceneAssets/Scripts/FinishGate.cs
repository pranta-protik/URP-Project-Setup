using Project;
using UnityEngine;

namespace DemoScene
{
	public class FinishGate : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag(ConstUtils.PLAYER_TAG))
			{
				GameManager.Instance.LevelCompleted();
			}
		}
	}
}