using MyTools.Utils;
using UnityEngine;

namespace Project
{
	public class PlayerCrouchState : PlayerBaseState
	{
		public class CrouchStateData
		{
			public CapsuleCollider capsuleCollider;
			public float crouchColliderHeight;
			public Vector3 crouchColliderCenter;
			public float crouchDeceleration;

			public CrouchStateData(CapsuleCollider capsuleCollider, float crouchColliderHeight, Vector3 crouchColliderCenter, float crouchDeceleration)
			{
				this.capsuleCollider = capsuleCollider;
				this.crouchColliderHeight = crouchColliderHeight;
				this.crouchColliderCenter = crouchColliderCenter;
				this.crouchDeceleration = crouchDeceleration;
			}
		}

		private readonly CrouchStateData _crouchStateData;

		public PlayerCrouchState(PlayerController playerController, Animator animator, CrouchStateData crouchStateData) : base(playerController, animator)
		{
			_crouchStateData = crouchStateData;
		}

		public override void OnEnter()
		{
			// DebugUtils.Log("PlayerCrouchState.OnEnter");

			_playerController.SetCrouchVelocity(_crouchStateData.crouchDeceleration);

			_crouchStateData.capsuleCollider.height = _crouchStateData.crouchColliderHeight;
			_crouchStateData.capsuleCollider.center = _crouchStateData.crouchColliderCenter;

			_animator.SetBool(_ISCrouching, true);
		}

		public override void FixedUpdate()
		{
			_playerController.HandleMovement();
		}

		public override void OnExit()
		{
			// DebugUtils.Log("PlayerCrouchState.OnExit");

			_playerController.SetCrouchVelocity(1f);
		}
	}
}