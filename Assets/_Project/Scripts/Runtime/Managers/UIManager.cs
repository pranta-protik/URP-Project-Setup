using System;
using System.Collections;
using MyTools.ES;
using MyTools.Utils;
using Project.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Managers
{
	public class UIManager : Singleton<UIManager>
	{
		[Header("References")]
		[SerializeField] private EventChannelSO levelStartChannel;
		[SerializeField] private IntEventChannelSO levelCompleteChannel;
		[SerializeField] private EventChannelSO levelFailChannel;
		[SerializeField, Range(0f, 15f)] private float _uiScreenDelay = 2f;

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
			levelStartChannel.Invoke(new Empty());
		}

		private void GameManager_OnLevelCompleted(int sceneIndex)
		{
			StartCoroutine(DelayActionRoutine(() => levelCompleteChannel.Invoke(sceneIndex)));
		}

		private void GameManager_OnLevelFailed()
		{
			StartCoroutine(DelayActionRoutine(() => levelFailChannel.Invoke(new Empty())));
		}

		private IEnumerator DelayActionRoutine(Action action)
		{
			yield return new WaitForSeconds(_uiScreenDelay);
			action.Invoke();
		}
	}
}