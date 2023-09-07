using KBCore.Refs;
using Project.IS;
using UnityEngine;

namespace Project.UI
{
	public class InventoryBarUI : ValidatedMonoBehaviour
	{
		[SerializeField, Anywhere] private Transform _itemSlotPrefab;

		private void Start()
		{
			InventorySystem.Instance.OnInventoryUpdated += InventorySystem_OnInventoryUpdated;
			InventorySystem_OnInventoryUpdated();
		}

		private void OnDestroy()
		{
			InventorySystem.Instance.OnInventoryUpdated -= InventorySystem_OnInventoryUpdated;
		}

		private void InventorySystem_OnInventoryUpdated()
		{
			foreach (Transform child in transform)
			{
				Destroy(child.gameObject);
			}

			DrawInventory();
		}

		private void DrawInventory()
		{
			foreach (var inventoryItem in InventorySystem.Instance.InventoryItemsList)
			{
				AddInventorySlot(inventoryItem);
			}
		}

		private void AddInventorySlot(InventoryItem item)
		{
			var itemSlotGO = Instantiate(_itemSlotPrefab, transform);

			var itemSlotUI = itemSlotGO.GetComponent<ItemSlotUI>();
			itemSlotUI.Set(item);
		}
	}
}