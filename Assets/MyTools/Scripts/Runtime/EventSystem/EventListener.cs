using UnityEngine;
using UnityEngine.Events;

namespace MyTools.ES
{
	public abstract class EventListener<T> : MonoBehaviour
	{
		[SerializeField] private EventChannelSO<T> eventChannel;
		[SerializeField] private UnityEvent<T> _unityEvent;

		protected void Awake()
		{
			eventChannel.Register(this);
		}

		private void OnDestroy()
		{
			eventChannel.Unregister(this);
		}

		public void Raise(T value)
		{
			_unityEvent?.Invoke(value);
		}
	}

	public class EventListener : EventListener<Empty> { }
}