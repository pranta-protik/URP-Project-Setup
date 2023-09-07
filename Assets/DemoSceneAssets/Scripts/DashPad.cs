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
			if (other.gameObject.TryGetComponent(out ICharacterActions characterActions))
			{
				OnDashPadInteraction?.Invoke();
				characterActions.Dash();
			}
		}
	}
}