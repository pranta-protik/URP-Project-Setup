using UnityEngine;

namespace Project
{
	[CreateAssetMenu(fileName = "InventoryItemData", menuName = "ScriptableObjects/InventoryItemData")]
	public class InventoryItemData : ScriptableObject
	{
		public string id;
		public string displayName;
		public Sprite icon;
		public GameObject prefab;
	}
}