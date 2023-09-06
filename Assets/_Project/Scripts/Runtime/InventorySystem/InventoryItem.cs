using System;

namespace Project
{
	[Serializable]
	public class InventoryItem
	{
		public InventoryItemData ItemData { get; private set; }
		public int StackSize { get; private set; }

		public InventoryItem(InventoryItemData itemData)
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