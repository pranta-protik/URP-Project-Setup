using MyTools.Utils;
using Project.Persistent;
using Project.Persistent.SaveSystem;
using UnityEngine.Events;

namespace Project.Managers
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
		public event UnityAction<int> OnLevelCompleted;
		public event UnityAction OnLevelFailed;

		public GameState CurrentGameState { get; private set; }

		protected override void OnAwake()
		{
			base.OnAwake();
			CurrentGameState = GameState.Waiting;
		}

		public void LevelStarted()
		{
			CurrentGameState = GameState.Running;
			OnLevelStarted?.Invoke();
		}

		public void LevelCompleted()
		{
			CurrentGameState = GameState.GameOver;
			OnLevelCompleted?.Invoke(LevelLoader.Instance.GetNextSceneIndex());
			DataPersistenceManager.Instance.SaveGame();
		}

		public void LevelFailed()
		{
			CurrentGameState = GameState.GameOver;
			OnLevelFailed?.Invoke();
			DataPersistenceManager.Instance.SaveGame();
		}
	}
}