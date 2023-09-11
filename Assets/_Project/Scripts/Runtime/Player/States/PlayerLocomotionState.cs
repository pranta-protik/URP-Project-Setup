using MyTools.Utils;
using UnityEngine;

namespace Project
{
	public class PlayerLocomotionState : PlayerBaseState
	{
		public class LocomotionStateData
		{
			public CapsuleCollider capsuleCollider;
			public float defaultColliderHeight;
			public Vector3 defaultColliderCenter;

			public LocomotionStateData(CapsuleCollider capsuleCollider, float defaultColliderHeight, Vector3 defaultColliderCenter)
			{
				this.capsuleCollider = capsuleCollider;
				this.defaultColliderHeight = defaultColliderHeight;
				this.defaultColliderCenter = defaultColliderCenter;
			}
		}

		private readonly LocomotionStateData _locomotionStateData;

		public PlayerLocomotionState(PlayerController playerController, Animator animator, LocomotionStateData locomotionStateData) : base(playerController, animator)
		{
			_locomotionStateData = locomotionStateData;
		}

		public override void OnEnter()
		{
			// DebugUtils.Log("PlayerLocomotionState.OnEnter");

			_locomotionStateData.capsuleCollider.height = _locomotionStateData.defaultColliderHeight;
			_locomotionStateData.capsuleCollider.center = _locomotionStateData.defaultColliderCenter;

			_animator.SetBool(_IsJumping, false);
			_animator.SetBool(_IsDashing, false);
			_animator.SetBool(_ISCrouching, false);
		}

		public override void FixedUpdate()
		{
			_playerController.HandleMovement();
			_playerController.HandleDrop();
		}
	}
}