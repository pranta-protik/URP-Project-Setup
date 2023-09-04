using MyTools;
using UnityEngine;
using UnityEngine.Events;

namespace Project
{
	public class GameManager : Singleton<GameManager>
	{
		public enum GameState
		{
			Waiting,
			Running,
			GameOver
		}

		public event UnityAction OnLevelStarted;
		public event UnityAction OnLevelCompleted;
		public event UnityAction OnLevelFailed;

		public GameState CurrentGameState { get; private set; }

		protected override void OnAwake()
		{
			CurrentGameState = GameState.Waiting;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.C)) LevelCompleted();
			if (Input.GetKeyDown(KeyCode.F)) LevelFailed();
		}

		public void LevelStarted()
		{
			CurrentGameState = GameState.Running;
			OnLevelStarted?.Invoke();
		}

		public void LevelCompleted()
		{
			CurrentGameState = GameState.GameOver;
			OnLevelCompleted?.Invoke();
		}

		public void LevelFailed()
		{
			CurrentGameState = GameState.GameOver;
			OnLevelFailed?.Invoke();
		}
	}
}