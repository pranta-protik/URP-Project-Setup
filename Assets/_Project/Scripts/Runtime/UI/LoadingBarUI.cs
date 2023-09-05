using MyTools;
using UnityEngine;
using UnityEngine.UI;
using KBCore.Refs;

namespace Project
{
	public class LoadingBarUI : ValidatedMonoBehaviour
	{
		[SerializeField, Anywhere] private Image _fillMask;
		[SerializeField, Range(0f, 10f)] private float _loadingDuration = 3f;

		private CountdownTimer _loadingTimer;

		private void Awake()
		{
			_fillMask.fillAmount = 0f;
			_loadingTimer = new CountdownTimer(_loadingDuration);
			_loadingTimer.OnTimerStop += () =>
			{
				SceneUtils.LoadSpecificScene((int)SceneIndex.GAME);
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