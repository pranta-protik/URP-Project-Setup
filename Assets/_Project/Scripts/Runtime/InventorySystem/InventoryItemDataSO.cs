using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.IS
{
	[InlineEditor(InlineEditorObjectFieldModes.Foldout)]
	[CreateAssetMenu(fileName = "InventoryItemData", menuName = "ScriptableObjects/InventorySystem/InventoryItemData")]
	public class InventoryItemDataSO : ScriptableObject
	{
		[PreviewField] public Sprite icon;
		public GameObject prefab;
		public string id;
		public string displayName;
		public int value;
	}
}