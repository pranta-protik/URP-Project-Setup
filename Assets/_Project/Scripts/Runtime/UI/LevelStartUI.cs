using UnityEngine;
using DG.Tweening;

namespace Project
{
	public class LevelStartUI : MonoBehaviour
	{
		[SerializeField] private Transform _handTransform;
		[SerializeField] private Vector3 _targetHandPosition;
		[SerializeField] private float _moveDuration = 1f;
		[SerializeField] private Transform _textTransform;
		[SerializeField] private float _targetTextScale = 1.2f;
		[SerializeField] private float _scaleDuration = 0.5f;

		private Vector3 _initialHandPosition;
		private Vector3 _initialTextScale;

		private void Awake()
		{
			_initialHandPosition = _handTransform.localPosition;
			_initialTextScale = _textTransform.localScale;
		}

		private void OnEnable()
		{
			_handTransform.DOLocalMove(_targetHandPosition, _moveDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
			_textTransform.DOScale(_textTransform.localScale * _targetTextScale, _scaleDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
		}

		private void OnDisable()
		{
			_handTransform.DOKill();
			_handTransform.localPosition = _initialHandPosition;
			_textTransform.DOKill();
			_textTransform.localScale = _initialTextScale;
		}
	}
}