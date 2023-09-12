using Sirenix.OdinInspector;
using Project.Persistent.SaveSystem;
using UnityEngine;
using KBCore.Refs;
using Project.IS;
using Project.Managers;
using DG.Tweening;

namespace Project.Unlockables
{
	public abstract class UnlockableItem : ValidatedMonoBehaviour, IUnlockable, IInteractable, IDataPersistence
	{
		[TabGroup("References")][SerializeField, Child] private PaymentPlatform _paymentPlatform;
		[TabGroup("References")][SerializeField, Child] private InteractionPlatform _interactionPlatform;
		[TabGroup("References")][SerializeField, Anywhere] protected Transform _idlingSpot;

		[TabGroup("Data Settings")][InlineButton("GenerateGUID")][SerializeField, LabelText("Id (Unique)")] private string _id;

		private void GenerateGUID() => _id = System.Guid.NewGuid().ToString();

		[TabGroup("Data Settings")][InlineEditor(InlineEditorObjectFieldModes.Foldout)][SerializeField] private InventoryItemDataSO _itemData;
		[TabGroup("Data Settings")][SerializeField] private int _unlockCost = 10;
		[TabGroup("Data Settings")][SerializeField] protected float _scaleTime = 1f;

		private int _depositedAmount;
		protected bool _isUnlocked;

		protected virtual void Start()
		{
			_isUnlocked = _depositedAmount >= _unlockCost;

			_paymentPlatform.gameObject.SetActive(!_isUnlocked);
			_interactionPlatform.gameObject.SetActive(_isUnlocked);
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

				if (_depositedAmount >= _unlockCost) UnlockItem();
			}
		}

		protected virtual void UnlockItem()
		{
			_paymentPlatform.gameObject.SetActive(false);
			_interactionPlatform.gameObject.SetActive(true);

			_interactionPlatform.transform.localScale = Vector3.zero;
			_interactionPlatform.transform.DOScale(Vector3.one, _scaleTime).SetEase(Ease.OutBack);
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

		public abstract void Interact(Transform interactor);

		public void LoadData(GameData gameData)
		{
			if (gameData.unlockableItemDictionary.TryGetValue(_id, out var depositedAmount))
			{
				_depositedAmount = depositedAmount;
			}
		}

		public void SaveData(GameData gameData)
		{
			if (gameData.unlockableItemDictionary.ContainsKey(_id))
			{
				gameData.unlockableItemDictionary.Remove(_id);
			}

			if (GameManager.Instance.IsGameOver()) return;

			gameData.unlockableItemDictionary.Add(_id, _depositedAmount);
		}

		protected virtual void OnDestroy() { }
	}
}