using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Project.IS
{
	[CreateAssetMenu(fileName = "InventoryItemDataList", menuName = "ScriptableObjects/InventorySystem/InventoryItemDataList")]
	public class InventoryItemDataListSO : ScriptableObject
	{
		[TableList] public List<InventoryItemDataSO> itemDataList;
	}
}