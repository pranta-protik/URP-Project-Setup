using Project;
using UnityEngine;

namespace DemoScene
{
	public class FinishGate : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag(ConstUtils.TAG_PLAYER))
			{
				GameManager.Instance.LevelCompleted();
			}
		}
	}
}