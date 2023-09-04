using MyTools;
using UnityEngine;

namespace Project
{
	public abstract class PlayerBaseState : IState
	{
		protected readonly PlayerController _playerController;
		protected readonly Animator _animator;
		protected static readonly int IsJumping = Animator.StringToHash("IsJumping");
		protected static readonly int IsDashing = Animator.StringToHash("IsDashing");

		protected PlayerBaseState(PlayerController playerController, Animator animator)
		{
			_playerController = playerController;
			_animator = animator;
		}

		public virtual void OnEnter()
		{
		}

		public virtual void Update()
		{
		}

		public virtual void FixedUpdate()
		{
		}

		public virtual void OnExit()
		{
			// DebugUtils.Log("PlayerBaseState.OnExit");
		}
	}
}