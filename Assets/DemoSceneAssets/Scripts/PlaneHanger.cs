using DG.Tweening;
using KBCore.Refs;
using Project;
using UnityEngine;

namespace DemoScene
{
	public class PlaneHanger : UnlockableItem
	{
		[SerializeField, Anywhere] private Transform _rampTransform;

		protected override void Start()
		{
			base.Start();
			_rampTransform.gameObject.SetActive(_isUnlocked);
		}

		protected override void UnlockItem()
		{
			base.UnlockItem();

			_rampTransform.gameObject.SetActive(true);
			_rampTransform.localScale = Vector3.zero;
			_rampTransform.DOScale(Vector3.one, _scaleTime).SetEase(Ease.OutBack);
		}

		public override void Interact(Transform interactor)
		{
			if (interactor)
			{
				CharacterSwitcher.Instance.SwitchToPlanePlayerCharacter();
				interactor.position = _idlingSpot.position;
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			_rampTransform.DOKill();
		}
	}
}