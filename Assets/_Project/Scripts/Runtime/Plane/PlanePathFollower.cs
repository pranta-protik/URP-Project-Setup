using KBCore.Refs;
using UnityEngine;
using UnityEngine.Splines;

namespace Project
{
	public class PlanePathFollower : ValidatedMonoBehaviour
	{
		private enum LoopMode
		{
			Once,
			Continuous
		}

		[SerializeField, Anywhere] private SplineContainer _spline;
		[SerializeField] private LoopMode _loopMode = LoopMode.Once;
		[SerializeField] private float _speed = 10f;

		private float _distanceTravelled;
		private float _splineLength;

		private void Start()
		{
			_splineLength = _spline.CalculateLength();
		}

		private void Update()
		{
			switch (_loopMode)
			{
				case LoopMode.Once:
					if (_distanceTravelled >= 1f)
					{
						return;
					}
					break;

				case LoopMode.Continuous:
					if (_distanceTravelled >= 1f)
					{
						_distanceTravelled = 0f;
					}
					break;
			}

			_distanceTravelled += _speed * Time.deltaTime / _splineLength;

			var currentPosition = _spline.EvaluatePosition(_distanceTravelled);
			transform.position = currentPosition;

			var currentTangent = _spline.EvaluateTangent(_distanceTravelled);
			transform.rotation = Quaternion.LookRotation(currentTangent);
		}
	}
}