using Project;
using UnityEngine;
using UnityEngine.Events;

namespace DemoScene
{
	public class DashPad : MonoBehaviour
	{
		public static event UnityAction OnDashPadInteraction;

		private void OnCollisionEnter(Collision other)
		{
			if (other.transform.TryGetComponent<ICharacterActions>(out ICharacterActions characterActions))
			{
				OnDashPadInteraction?.Invoke();
				characterActions.Dash();
			}
		}
	}
}