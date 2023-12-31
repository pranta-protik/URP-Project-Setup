using System.Collections.Generic;
using UnityEngine;

namespace MyTools.ES
{
	public abstract class EventChannelSO<T> : ScriptableObject
	{
		private readonly HashSet<EventListener<T>> _observersSet = new();

		public void Invoke(T value)
		{
			foreach (var observer in _observersSet)
			{
				observer.Raise(value);
			}
		}

		public void Register(EventListener<T> observer) => _observersSet.Add(observer);
		public void Unregister(EventListener<T> observer) => _observersSet.Remove(observer);
	}

	public readonly struct Empty { }

	[CreateAssetMenu(menuName = "ScriptableObjects/Events/EventChannel", fileName = "EventChannel")]
	public class EventChannelSO : EventChannelSO<Empty> { }
}