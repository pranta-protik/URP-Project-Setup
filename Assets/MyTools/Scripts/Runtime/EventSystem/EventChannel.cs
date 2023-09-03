using System.Collections.Generic;
using UnityEngine;

namespace MyTools
{
	public abstract class EventChannel<T> : ScriptableObject
	{
		private readonly HashSet<EventListener<T>> _observers = new();

		public void Invoke(T value)
		{
			foreach (var observer in _observers)
			{
				observer.Raise(value);
			}
		}

		public void Register(EventListener<T> observer) => _observers.Add(observer);
		public void Unregister(EventListener<T> observer) => _observers.Remove(observer);
	}

	public readonly struct Empty { }

	[CreateAssetMenu(menuName = "ScriptableObjects/Events/EventChannel")]
	public class EventChannel : EventChannel<Empty> { }
}