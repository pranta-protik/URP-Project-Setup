using DG.Tweening;
using Project;
using UnityEngine;
using UnityEngine.Events;

namespace DemoScene
{
	public class JumpPad : MonoBehaviour
	{
		public static event UnityAction OnJumpPadInteraction;

		[SerializeField] private float _targetScale = 0.6f;
		[SerializeField] private float _scaleDuration = 1f;

		private Vector3 _initialScale;

		private void Awake()
		{
			_initialScale = transform.localScale;
		}

		private void OnCollisionEnter(Collision other)
		{
			if (other.transform.TryGetComponent<ICharacterActions>(out ICharacterActions characterActions))
			{
				OnJumpPadInteraction?.Invoke();

				characterActions.Jump();

				transform.DOKill();
				transform.localScale = _initialScale;

				transform.DOShakeScale(_scaleDuration, _initialScale * _targetScale, 5, 30, true, ShakeRandomnessMode.Harmonic);
			}
		}

		private void OnDestroy()
		{
			transform.DOKill();
		}
	}
}