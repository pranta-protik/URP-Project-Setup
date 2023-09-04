using MyTools;
using UnityEngine;
using UnityEngine.Events;

namespace Project
{
	public class GameManager : Singleton<GameManager>
	{
		public event UnityAction OnLevelStart;
		public event UnityAction OnLevelComplete;
		public event UnityAction OnLevelFail;

		[SerializeField] private EventChannel _levelStartChannel;

		public void LevelStart()
		{
			_levelStartChannel?.Invoke(new Empty());
		}
	}
}