using System.Collections.Generic;
using UnityEngine;

namespace Project.IS
{
	[CreateAssetMenu(fileName = "InventoryItemDataList", menuName = "ScriptableObjects/InventorySystem/InventoryItemDataList")]
	public class InventoryItemDataListSO : ScriptableObject
	{
		public List<InventoryItemDataSO> itemDataList;
	}
}