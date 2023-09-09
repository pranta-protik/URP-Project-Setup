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

		public InventoryItem(InventoryItemDataSO itemData, int stackSize)
		{
			ItemData = itemData;
			StackSize = stackSize;
		}

		public void AddToStack()
		{
			StackSize++;
		}

		public void RemoveFromStack(int amount)
		{
			StackSize -= amount;
		}

		public int GetTotalValue()
		{
			return StackSize * ItemData.value;
		}
	}
}