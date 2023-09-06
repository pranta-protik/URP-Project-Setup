using UnityEngine;

namespace Project
{
	public class PlayerDashState : PlayerBaseState
	{
		public PlayerDashState(PlayerController playerController, Animator animator) : base(playerController, animator)
		{
		}

		public override void OnEnter()
		{
			// DebugUtils.Log("PlayerDashState.OnEnter");
			_animator.SetBool(_IsDashing, true);
		}

		public override void FixedUpdate()
		{
			_playerController.HandleMovement();
		}
	}
}