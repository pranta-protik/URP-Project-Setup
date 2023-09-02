using UnityEngine;
using DG.Tweening;

namespace Project
{
	public class LevelStartUI : MonoBehaviour
	{
		[SerializeField] private Transform _handTransform;
		[SerializeField] private Vector3 _targetPosition;
		[SerializeField] private float _moveDuration = 1f;
		[SerializeField] private Transform _textTransform;
		[SerializeField] private float _targetScale = 1.2f;
		[SerializeField] private float _scaleDuration = 0.5f;


		private void OnEnable()
		{
			_handTransform.DOLocalMove(_targetPosition, _moveDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
			_textTransform.DOScale(_textTransform.localScale * _targetScale, _scaleDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
		}

		private void OnDisable()
		{
			_handTransform.DOKill();
			_textTransform.DOKill();
		}
	}
}