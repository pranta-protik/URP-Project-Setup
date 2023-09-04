using Project;
using UnityEngine;

namespace DemoScene
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