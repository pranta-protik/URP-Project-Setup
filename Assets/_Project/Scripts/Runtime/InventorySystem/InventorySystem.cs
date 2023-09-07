using System.Collections.Generic;
using MyTools.Utils;
using Project.Persistent.SaveSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Project.IS
{
	public class InventorySystem : Singleton<InventorySystem>, IDataPersistence
	{
		public event UnityAction OnInventoryUpdated;

		[SerializeField] private InventoryItemDataListSO _inventoryItemDataList;

		private Dictionary<string, InventoryItemDataSO> _inventoryItemDataDictionary;
		private Dictionary<InventoryItemDataSO, InventoryItem> _inventoryItemsDictionary;
		public List<InventoryItem> InventoryItemsList { get; private set; }

		protected override void OnAwake()
		{
			base.OnAwake();

			_inventoryItemDataDictionary = new Dictionary<string, InventoryItemDataSO>();

			foreach (var itemData in _inventoryItemDataList.itemDataList)
			{
				_inventoryItemDataDictionary.Add(itemData.name, itemData);
			}

			_inventoryItemsDictionary = new Dictionary<InventoryItemDataSO, InventoryItem>();
			InventoryItemsList = new List<InventoryItem>();
		}

		public InventoryItem Get(InventoryItemDataSO itemData)
		{
			if (_inventoryItemsDictionary.TryGetValue(itemData, out InventoryItem item))
			{
				return item;
			}

			return null;
		}

		public void Add(InventoryItemDataSO itemData)
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

		public void Remove(InventoryItemDataSO itemData)
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

		public void LoadData(GameData gameData)
		{
		}

		public void SaveData(GameData gameData)
		{

		}
	}
}