using KBCore.Refs;
using UnityEngine;

namespace Project
{
	public class InventoryItemObject : ValidatedMonoBehaviour
	{
		[Header("References")]
		[SerializeField] private InventoryItemData _itemData;

		public void HandleItemPickup()
		{
			InventorySystem.Instance.Add(_itemData);
			gameObject.SetActive(false);
		}
	}
}