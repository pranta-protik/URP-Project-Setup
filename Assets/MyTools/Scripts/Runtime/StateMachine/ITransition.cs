namespace MyTools.SM
{
	public interface ITransition
	{
		public IState To { get; }
		public IPredicate Condition { get; }
	}
}