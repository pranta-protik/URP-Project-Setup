using MyTools;
using UnityEngine;

namespace Project
{
	public class PlayerLocomotionState : PlayerBaseState
	{
		public PlayerLocomotionState(PlayerController playerController, Animator animator) : base(playerController, animator)
		{
		}

		public override void OnEnter()
		{
			// DebugUtils.Log("PlayerLocomotionState.OnEnter");
			_animator.SetBool(IsJumping, false);
			_animator.SetBool(IsDashing, false);
		}

		public override void FixedUpdate()
		{
			_playerController.HandleMovement();
		}
	}
}