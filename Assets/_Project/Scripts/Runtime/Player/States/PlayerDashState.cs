using MyTools.Utils;
using UnityEngine;

namespace Project
{
	public class PlayerDashState : PlayerBaseState
	{
		public class DashStateData
		{
			public CapsuleCollider capsuleCollider;
			public float dashColliderHeight;
			public Vector3 dashColliderCenter;

			public DashStateData(CapsuleCollider capsuleCollider, float dashColliderHeight, Vector3 dashColliderCenter)
			{
				this.capsuleCollider = capsuleCollider;
				this.dashColliderHeight = dashColliderHeight;
				this.dashColliderCenter = dashColliderCenter;
			}
		}

		private readonly DashStateData _dashStateData;

		public PlayerDashState(PlayerController playerController, Animator animator, DashStateData dashStateData) : base(playerController, animator)
		{
			_dashStateData = dashStateData;
		}

		public override void OnEnter()
		{
			// DebugUtils.Log("PlayerDashState.OnEnter");

			_dashStateData.capsuleCollider.height = _dashStateData.dashColliderHeight;
			_dashStateData.capsuleCollider.center = _dashStateData.dashColliderCenter;

			_animator.SetBool(_IsDashing, true);
		}

		public override void FixedUpdate()
		{
			_playerController.HandleMovement();
		}
	}
}