namespace Project
{
	public interface IUnlockable
	{
		public void Deposit(int amount);
		public bool CanDeposit();
		public float GetDepositedAmountNormalized();
	}
}