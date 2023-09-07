using UnityEngine;

namespace Project.IS
{
	[CreateAssetMenu(fileName = "InventoryItemData", menuName = "ScriptableObjects/InventorySystem/InventoryItemData")]
	public class InventoryItemDataSO : ScriptableObject
	{
		public string id;
		public string displayName;
		public Sprite icon;
		public GameObject prefab;
	}
}