using UnityEngine;

namespace MyTools.Utils
{
	public class Rotator : MonoBehaviour
	{
		[SerializeField] private Vector3 _rotationAxis = Vector3.forward;
		[SerializeField] private float _rotationSpeed = 200f;

		private bool _canRotate = true;

		private void Update()
		{
			if (_canRotate) transform.Rotate(_rotationAxis * (_rotationSpeed * Time.deltaTime));
		}

		public void ToggleRotation(bool enable) => _canRotate = enable;
	}
}