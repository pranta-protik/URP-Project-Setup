using UnityEngine;
using DG.Tweening;

namespace Project
{
	public class LevelStartUI : MonoBehaviour
	{
		[SerializeField] private Transform _handTransform;
		[SerializeField] private Vector3 _moveTo = Vector3.zero;
		[SerializeField] private float _moveTime = 1f;
		[SerializeField] private Ease _moveEase = Ease.InOutSine;
		[SerializeField] private Transform _textTransform;
		[SerializeField] private float _scaleTo = 1.2f;
		[SerializeField] private float _scaleTime = 0.5f;
		[SerializeField] private Ease _scaleEase = Ease.InOutSine;

		private Vector3 _startPosition;
		private Vector3 _startScale;

		private void Awake()
		{
			_startPosition = _handTransform.localPosition;
			_startScale = _textTransform.localScale;
		}

		private void OnEnable()
		{
			_handTransform.DOLocalMove(_moveTo, _moveTime).SetEase(_moveEase).SetLoops(-1, LoopType.Yoyo);
			_textTransform.DOScale(_textTransform.localScale * _scaleTo, _scaleTime).SetEase(_scaleEase).SetLoops(-1, LoopType.Yoyo);
		}

		private void OnDisable()
		{
			_handTransform.DOKill();
			_handTransform.localPosition = _startPosition;
			_textTransform.DOKill();
			_textTransform.localScale = _startScale;
		}
	}
}