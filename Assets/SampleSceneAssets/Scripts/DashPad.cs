using Project;
using UnityEngine;

namespace SampleScene
{
	public class DashPad : MonoBehaviour
	{
		private void OnCollisionEnter(Collision other)
		{
			if (other.transform.TryGetComponent<ICharacterActions>(out ICharacterActions characterActions))
			{
				characterActions.Dash();
			}
		}
	}
}