using System;
using System.Collections;
using MyTools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
	public class UIManager : Singleton<UIManager>
	{
		[SerializeField, Range(0f, 15f)] private float _uiScreenDelay = 2f;
		[SerializeField] private EventChannel _levelStartChannel;
		[SerializeField] private IntEventChannel _levelCompleteChannel;
		[SerializeField] private EventChannel _levelFailChannel;

		private void Start()
		{
			GameManager.Instance.OnLevelStarted += GameManager_OnLevelStarted;
			GameManager.Instance.OnLevelCompleted += GameManager_OnLevelCompleted;
			GameManager.Instance.OnLevelFailed += GameManager_OnLevelFailed;

			if (!SceneManager.GetSceneByBuildIndex((int)SceneIndex.UI).isLoaded)
			{
				SceneUtils.LoadSpecificScene((int)SceneIndex.UI, LoadSceneMode.Additive);
			}
		}

		private void OnDestroy()
		{
			GameManager.Instance.OnLevelStarted -= GameManager_OnLevelStarted;
			GameManager.Instance.OnLevelCompleted -= GameManager_OnLevelCompleted;
			GameManager.Instance.OnLevelFailed -= GameManager_OnLevelFailed;
		}

		private void GameManager_OnLevelStarted()
		{
			_levelStartChannel?.Invoke(new Empty());
		}

		private void GameManager_OnLevelCompleted()
		{
			StartCoroutine(DelayAction(() => _levelCompleteChannel?.Invoke(LevelManager.Instance.GetNextSceneIndex())));
		}

		private void GameManager_OnLevelFailed()
		{
			StartCoroutine(DelayAction(() => _levelFailChannel?.Invoke(new Empty())));
		}

		private IEnumerator DelayAction(Action action)
		{
			yield return new WaitForSeconds(_uiScreenDelay);
			action.Invoke();
		}
	}
}