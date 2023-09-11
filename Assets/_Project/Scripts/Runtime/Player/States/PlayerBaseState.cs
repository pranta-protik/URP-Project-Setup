using MyTools.SM;
using MyTools.Utils;
using UnityEngine;

namespace Project
{
	public abstract class PlayerBaseState : IState
	{
		protected static readonly int _IsJumping = Animator.StringToHash("IsJumping");
		protected static readonly int _IsDashing = Animator.StringToHash("IsDashing");
		protected static readonly int _ISCrouching = Animator.StringToHash("IsCrouching");

		protected readonly PlayerController _playerController;
		protected readonly Animator _animator;

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