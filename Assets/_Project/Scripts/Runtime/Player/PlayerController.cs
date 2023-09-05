using System.Collections.Generic;
using Cinemachine;
using KBCore.Refs;
using MyTools;
using UnityEngine;

namespace Project
{
	public class PlayerController : ValidatedMonoBehaviour, ICharacterActions, IDataPersistence
	{
		[Header("References")]
		[SerializeField, Self] private Rigidbody _rigidbody;
		[SerializeField, Child] private Animator _animator;
		[SerializeField, Self] private GroundChecker _groundChecker;
		[SerializeField, Anywhere] private Joystick _joystick;
		[SerializeField, Anywhere] private CinemachineVirtualCamera _playerVCam;

		[Header("Movement Settings")]
		[SerializeField] private float _moveSpeed = 300f;
		[SerializeField] private float _rotationSpeed = 300f;
		[SerializeField] private float _stoppingSpeed = 3f;
		[SerializeField] private float _smoothTime = 0.2f;

		[Header("Jump Settings")]
		[SerializeField] private float _jumpForce = 10f;
		[SerializeField] private float _jumpDuration = 0.5f;
		[SerializeField] private float _gravityMultiplier = 3f;

		[Header("Dash Settings")]
		[SerializeField] private float _dashForce = 10f;
		[SerializeField] private float _dashDuration = 0.5f;

		private float _currentSpeed;
		private float _velocity;
		private float _jumpVelocity;
		private float _dashVelocity = 1f;
		private Vector3 _moveDir;
		private List<Timer> _timersList;
		private CountdownTimer _jumpTimer;
		private CountdownTimer _dashTimer;
		private StateMachine _stateMachine;
		private static readonly int Speed = Animator.StringToHash("Speed");

		private const float ZERO_F = 0f;

		private void Awake()
		{
			_playerVCam.Follow = transform;

			_rigidbody.freezeRotation = true;

			SetupTimers();
			SetupStateMachine();
		}

		private void SetupTimers()
		{
			_jumpTimer = new CountdownTimer(_jumpDuration);
			_jumpTimer.OnTimerStart += () => _jumpVelocity = _jumpForce;

			_dashTimer = new CountdownTimer(_dashDuration);
			_dashTimer.OnTimerStart += () => _dashVelocity = _dashForce;
			_dashTimer.OnTimerStop += () => _dashVelocity = 1f;

			_timersList = new List<Timer>(2) { _jumpTimer, _dashTimer };
		}

		private void SetupStateMachine()
		{
			_stateMachine = new StateMachine();

			// Declate States
			var locomotionState = new PlayerLocomotionState(this, _animator);
			var jumpState = new PlayerJumpState(this, _animator);
			var dashState = new PlayerDashState(this, _animator);

			// Declate Transitions
			At(locomotionState, jumpState, new FuncPredicate(() => _jumpTimer.IsRunning));
			At(locomotionState, dashState, new FuncPredicate(() => _dashTimer.IsRunning));
			At(dashState, jumpState, new FuncPredicate(() => _jumpTimer.IsRunning));

			Any(locomotionState, new FuncPredicate(() => _groundChecker.IsGrounded && !_jumpTimer.IsRunning && !_dashTimer.IsRunning));

			// Set initial state
			_stateMachine.SetState(locomotionState);
		}

		private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
		private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

		private void Update()
		{
			if (GameManager.Instance.CurrentGameState == GameManager.GameState.Running)
			{
				_moveDir = new Vector3(_joystick.Direction.x, 0f, _joystick.Direction.y);
			}
			else
			{
				_moveDir = Vector3.Lerp(_moveDir, Vector3.zero, _stoppingSpeed * Time.deltaTime);
			}

			_stateMachine.Update();

			HandleTimers();
			UpdateAnimator();
		}

		private void HandleTimers()
		{
			foreach (var timer in _timersList)
			{
				timer.Tick(Time.deltaTime);
			}
		}

		private void UpdateAnimator()
		{
			_animator.SetFloat(Speed, _currentSpeed);
		}

		private void FixedUpdate()
		{
			_stateMachine.FixedUpdate();
		}

		public void HandleMovement()
		{
			if (_moveDir.magnitude > ZERO_F)
			{
				HandleRotation();
				HandleHorizontalMovement();
				SmoothSpeed(_moveDir.magnitude);
			}
			else
			{
				SmoothSpeed(ZERO_F);

				// Reset horizontal velocity for a snappy stop
				_rigidbody.velocity = new Vector3(ZERO_F, _rigidbody.velocity.y, ZERO_F);
			}
		}

		private void HandleRotation()
		{
			if (_moveDir != Vector3.zero)
			{
				var targetRotation = Quaternion.LookRotation(_moveDir);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
			}
		}

		private void HandleHorizontalMovement()
		{
			var velocity = _dashVelocity * _moveSpeed * Time.fixedDeltaTime * _moveDir;
			_rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
		}

		public void Jump()
		{
			_jumpTimer.Start();
		}

		public void HandleJump()
		{
			if (!_jumpTimer.IsRunning && _groundChecker.IsGrounded)
			{
				_jumpVelocity = ZERO_F;
				_jumpTimer.Stop();
				return;
			}

			if (!_jumpTimer.IsRunning)
			{
				// Gravity takes over
				_jumpVelocity += Physics.gravity.y * _gravityMultiplier * Time.fixedDeltaTime;
			}

			_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpVelocity, _rigidbody.velocity.z);
		}

		public void Dash()
		{
			_dashTimer.Start();
		}

		private void SmoothSpeed(float value)
		{
			_currentSpeed = Mathf.SmoothDamp(_currentSpeed, value, ref _velocity, _smoothTime);
		}

		public void LoadData(GameData gameData)
		{
			transform.position = gameData.playerPosition;
		}

		public void SaveData(GameData gameData)
		{
			if (GameManager.Instance.CurrentGameState == GameManager.GameState.GameOver)
			{
				gameData.playerPosition = Vector3.zero;
			}
			else
			{
				gameData.playerPosition = transform.position;
			}
		}
	}
}