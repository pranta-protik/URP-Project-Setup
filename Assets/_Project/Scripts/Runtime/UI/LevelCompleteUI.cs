using DG.Tweening;
using MyTools;
using UnityEngine;

namespace Project
{
	public class LevelCompleteUI : MonoBehaviour
	{
		[SerializeField] private Transform _buttonTransform;
		[SerializeField] private float _ScaleTo = 1.1f;
		[SerializeField] private float _scaleTime = 0.5f;
		[SerializeField] private Ease _ease = Ease.InOutSine;

		private Vector3 _startScale;
		private int _nextSceneIndex;

		private void Awake()
		{
			_startScale = _buttonTransform.localScale;
		}

		private void OnEnable()
		{
			_buttonTransform.DOScale(_buttonTransform.localScale * _ScaleTo, _scaleTime).SetEase(_ease).SetLoops(-1, LoopType.Yoyo);
		}

		private void OnDisable()
		{
			_buttonTransform.DOKill();
			_buttonTransform.localScale = _startScale;
		}

		public void SetNextSceneIndex(int sceneIndex) => _nextSceneIndex = sceneIndex;
		public void LoadNextScene() => SceneUtils.LoadSpecificScene(_nextSceneIndex);
	}
}