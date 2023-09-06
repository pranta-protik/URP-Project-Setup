using System.Collections.Generic;
using MyTools;
using UnityEngine.Events;

namespace Project
{
	public class InventorySystem : Singleton<InventorySystem>
	{
		public event UnityAction OnInventoryUpdated;

		private Dictionary<InventoryItemData, InventoryItem> _inventoryItemsDictionary;
		public List<InventoryItem> InventoryItemsList { get; private set; }

		protected override void OnAwake()
		{
			base.OnAwake();

			InventoryItemsList = new List<InventoryItem>();
			_inventoryItemsDictionary = new Dictionary<InventoryItemData, InventoryItem>();
		}

		public InventoryItem Get(InventoryItemData itemData)
		{
			if (_inventoryItemsDictionary.TryGetValue(itemData, out InventoryItem item))
			{
				return item;
			}

			return null;
		}

		public void Add(InventoryItemData itemData)
		{
			if (_inventoryItemsDictionary.TryGetValue(itemData, out InventoryItem item))
			{
				item.AddToStack();
			}
			else
			{
				var newItem = new InventoryItem(itemData);
				InventoryItemsList.Add(newItem);
				_inventoryItemsDictionary.Add(itemData, newItem);
			}

			OnInventoryUpdated?.Invoke();
		}

		public void Remove(InventoryItemData itemData)
		{
			if (_inventoryItemsDictionary.TryGetValue(itemData, out InventoryItem item))
			{
				item.RemoveFromStack();

				if (item.StackSize == 0)
				{
					InventoryItemsList.Remove(item);
					_inventoryItemsDictionary.Remove(itemData);
				}
			}

			OnInventoryUpdated?.Invoke();
		}
	}
}