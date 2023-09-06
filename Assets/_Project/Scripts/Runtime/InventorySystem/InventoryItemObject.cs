using UnityEngine;

namespace Project
{
	public abstract class InventoryItemObject : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private InventoryItemData _itemData;
		public InventoryItemData ItemData => _itemData;
		public bool IsPickedUp { get; set; }

		public void HandleItemPickup()
		{
			InventorySystem.Instance.Add(_itemData);
			IsPickedUp = true;
			gameObject.SetActive(false);
		}
	}
}