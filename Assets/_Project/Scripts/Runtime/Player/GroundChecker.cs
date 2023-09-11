using UnityEngine;

namespace Project
{
	public class GroundChecker : MonoBehaviour
	{
		[SerializeField] private float _groundDistance = 0.3f;
		[SerializeField] private float _sphereRadius = 0.02f;
		[SerializeField] private LayerMask _groundLayers;

		public bool IsGrounded { get; private set; }

		private void Update()
		{
			IsGrounded = Physics.SphereCast(transform.position, _sphereRadius, Vector3.down, out _, _groundDistance, _groundLayers);
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, _sphereRadius);
			Gizmos.DrawWireSphere(transform.position + Vector3.down * _groundDistance, _sphereRadius);
			Gizmos.color = Color.white;
		}
#endif
	}
}