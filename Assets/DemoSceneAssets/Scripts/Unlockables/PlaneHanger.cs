using KBCore.Refs;
using Project.IS;
using UnityEngine;
using Project.Persistent.SaveSystem;
using Project.Managers;
using DG.Tweening;
using Project;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DemoScene
{
	public class PlaneHanger : ValidatedMonoBehaviour, IUnlockable, IInteractable, IDataPersistence
	{
		[Header("Id has to be unique")]
		[SerializeField] private string _id;

#if UNITY_EDITOR
		[ContextMenu("Generate GUID for ID")]
		private void GenerateGUID()
		{
			_id = System.Guid.NewGuid().ToString();
			EditorUtility.SetDirty(gameObject);
		}
#endif

		[Header("References")]
		[SerializeField, Child] private PaymentPlatform _paymentPlatform;
		[SerializeField, Child] private InteractionPlatform _interactionPlatform;
		[SerializeField, Anywhere] private Transform _idlingSpot;
		[SerializeField, Anywhere] private Transform _rampTransform;

		[Header("Data Settings")]
		[SerializeField] private InventoryItemDataSO _itemData;
		[SerializeField] private int _unlockCost = 10;
		[SerializeField] private float _scaleTime = 1f;

		private int _depositedAmount;
		private bool _isUnlocked;

		private void Start()
		{
			_isUnlocked = _depositedAmount >= _unlockCost;

			_paymentPlatform.gameObject.SetActive(!_isUnlocked);
			_interactionPlatform.gameObject.SetActive(_isUnlocked);
			_rampTransform.gameObject.SetActive(_isUnlocked);
		}

		public void Deposit(int amount)
		{
			if (CanDeposit())
			{
				if (amount > InventorySystem.Instance.Get(_itemData).StackSize)
				{
					amount = InventorySystem.Instance.Get(_itemData).StackSize;
				}

				_depositedAmount += amount;
				InventorySystem.Instance.Remove(_itemData, amount);

				if (_depositedAmount >= _unlockCost) UnlockHanger();
			}
		}

		private void UnlockHanger()
		{
			_paymentPlatform.gameObject.SetActive(false);

			_interactionPlatform.gameObject.SetActive(true);
			_interactionPlatform.transform.localScale = Vector3.zero;
			_interactionPlatform.transform.DOScale(Vector3.one, _scaleTime).SetEase(Ease.OutBack);

			_rampTransform.gameObject.SetActive(true);
			_rampTransform.localScale = Vector3.zero;
			_rampTransform.DOScale(Vector3.one, _scaleTime).SetEase(Ease.OutBack);
		}

		public bool CanDeposit()
		{
			if (_depositedAmount < _unlockCost && InventorySystem.Instance.Get(_itemData) != null)
			{
				if (InventorySystem.Instance.Get(_itemData).StackSize > 0)
				{
					return true;
				}
			}

			return false;
		}

		public float GetDepositedAmountNormalized() => (float)_depositedAmount / _unlockCost;

		public void Interact(Transform interactor)
		{
			if (interactor)
			{
				CharacterSwitcher.Instance.SwitchToPlanePlayerCharacter();
				interactor.position = _idlingSpot.position;
			}
		}

		public void LoadData(GameData gameData)
		{
			if (gameData.planeHangerDictionary.TryGetValue(_id, out var depositedAmount))
			{
				_depositedAmount = depositedAmount;
			}
		}

		public void SaveData(GameData gameData)
		{
			if (gameData.planeHangerDictionary.ContainsKey(_id))
			{
				gameData.planeHangerDictionary.Remove(_id);
			}

			if (GameManager.Instance.IsGameOver()) return;

			gameData.planeHangerDictionary.Add(_id, _depositedAmount);
		}

		private void OnDestroy() => _rampTransform.DOKill();
	}
}