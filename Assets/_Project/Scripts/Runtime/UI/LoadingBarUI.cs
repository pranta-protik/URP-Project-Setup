using Project;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MyTools
{
	public class LoadingBarUI : MonoBehaviour
	{
		[SerializeField] private Image _fillMask;
		[SerializeField, Range(0f, 10f)] private float _loadingDuration = 3f;

		private CountdownTimer _loadingTimer;

		private void Awake()
		{
			_fillMask.fillAmount = 0f;
			_loadingTimer = new CountdownTimer(_loadingDuration);
			_loadingTimer.OnTimerStop += () =>
			{
				SceneUtils.LoadSpecificScene((int)SceneIndex.GAME).completed += (_) =>
				{
					SceneUtils.LoadSpecificScene((int)SceneIndex.UI, LoadSceneMode.Additive);
				};
			};
		}

		private void Start() => _loadingTimer.Start();

		private void Update()
		{
			_loadingTimer.Tick(Time.deltaTime);
			_fillMask.fillAmount = _loadingTimer.Progress;
		}
	}
}