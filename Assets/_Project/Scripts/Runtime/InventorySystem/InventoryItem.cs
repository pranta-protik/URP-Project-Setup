using System;

namespace Project.IS
{
	[Serializable]
	public class InventoryItem
	{
		public InventoryItemDataSO ItemData { get; private set; }
		public int StackSize { get; private set; }

		public InventoryItem(InventoryItemDataSO itemData)
		{
			ItemData = itemData;
			AddToStack();
		}

		public void AddToStack()
		{
			StackSize++;
		}

		public void RemoveFromStack()
		{
			StackSize--;
		}
	}
}