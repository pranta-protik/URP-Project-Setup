using System;
using MyTools;
using UnityEngine;

namespace Project
{
	public class GameManager : Singleton<GameManager>
	{
		public event Action OnLevelStart;
		public event Action OnLevelComplete;
		public event Action OnLevelFail;

		[SerializeField] private EventChannel _levelStartChannel;

		public void LevelStart()
		{
			_levelStartChannel?.Invoke(new Empty());
		}
	}
}