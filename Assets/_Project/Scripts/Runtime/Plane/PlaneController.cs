using Sirenix.OdinInspector;
using Cinemachine;
using KBCore.Refs;
using Project.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Project
{
	public class PlaneController : ValidatedMonoBehaviour
	{
		[TabGroup("References")][SerializeField, Anywhere] private Joystick _joystick;
		[TabGroup("References")][SerializeField, Anywhere] private CinemachineVirtualCamera _planeVCam;

		[TabGroup("Movement Settings")][SerializeField] private Vector2 _movementLimit = new(2f, 2f);
		[TabGroup("Movement Settings")][SerializeField] private float _movementRange = 5f;
		[TabGroup("Movement Settings")][SerializeField] private float _movementSpeed = 10f;
		[TabGroup("Movement Settings")][SerializeField] private float _maxRoll = 15f;
		[TabGroup("Movement Settings")][SerializeField] private float _rollSpeed = 5f;
		[TabGroup("Movement Settings")][SerializeField] private float _maxPitch = 10f;
		[TabGroup("Movement Settings")][SerializeField] private float _pitchSpeed = 5f;

		public UnityEvent OnEjected;

		private Vector3 _targetPosition;
		private float _roll;
		private float _pitch;

		private void Awake()
		{
			_planeVCam.Follow = transform;
			_planeVCam.LookAt = transform;

			_targetPosition = transform.localPosition;
		}

		private void Update()
		{
			if (GameManager.Instance.CurrentGameState == GameManager.GameState.Running)
			{
				HandleMovement();
			}
		}

		private void HandleMovement()
		{
			_targetPosition.x += _joystick.Direction.x * _movementSpeed * _movementRange * Time.deltaTime;
			_targetPosition.y += _joystick.Direction.y * _movementSpeed * _movementRange * Time.deltaTime;

			_targetPosition.x = Mathf.Clamp(_targetPosition.x, -_movementLimit.x, _movementLimit.x);
			_targetPosition.y = Mathf.Clamp(_targetPosition.y, -_movementLimit.y, _movementLimit.y);

			transform.localPosition = _targetPosition;

			_roll = Mathf.Lerp(_roll, -_joystick.Direction.x * _maxRoll, _rollSpeed * Time.deltaTime);
			_pitch = Mathf.Lerp(_pitch, -_joystick.Direction.y * _maxPitch, _pitchSpeed * Time.deltaTime);

			transform.localRotation = Quaternion.Euler(_pitch, transform.localEulerAngles.y, _roll);
		}

		private void ResetOrientation()
		{
			_roll = 0f;
			_pitch = 0f;
			_targetPosition = Vector3.zero;

			transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
		}

		public void OnEjectButtonClick()
		{
			OnEjected?.Invoke();
			ResetOrientation();
			CharacterSwitcher.Instance.SwitchToHumanPlayerCharacter();
		}
	}
}