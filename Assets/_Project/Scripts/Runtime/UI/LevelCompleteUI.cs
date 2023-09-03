using DG.Tweening;
using UnityEngine;

namespace Project
{
	public class LevelCompleteUI : MonoBehaviour
	{
		[SerializeField] private Transform _buttonTransform;
		[SerializeField] private float _targetButtonScale = 1.1f;
		[SerializeField] private float _scaleDuration = 0.5f;

		private Vector3 _initialButtonScale;

		private void Awake()
		{
			_initialButtonScale = _buttonTransform.localScale;
		}

		private void OnEnable()
		{
			_buttonTransform.DOScale(_buttonTransform.localScale * _targetButtonScale, _scaleDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
		}

		private void OnDisable()
		{
			_buttonTransform.DOKill();
			_buttonTransform.localScale = _initialButtonScale;
		}
	}
}