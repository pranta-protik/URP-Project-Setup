using Sirenix.OdinInspector;
using DemoScene;
using MyTools.Utils;
using UnityEngine;

namespace Project.Managers
{
	public class HapticManager : MonoBehaviour
	{
		private enum LogState
		{
			Enable,
			Disable
		}

		[EnumToggleButtons, Title("Log Status"), HideLabel][SerializeField] private LogState _logState = LogState.Enable;

		private void Awake() => HapticUtils.SetLogStatus(_logState == LogState.Enable);

		private void Start()
		{
			JumpPad.OnJumpPadInteraction += JumpPad_OnJumpPadInteraction;
			DashPad.OnDashPadInteraction += DashPad_OnDashPadInteraction;
		}

		private void OnDestroy()
		{
			JumpPad.OnJumpPadInteraction -= JumpPad_OnJumpPadInteraction;
			DashPad.OnDashPadInteraction -= DashPad_OnDashPadInteraction;
		}

		private void JumpPad_OnJumpPadInteraction() => HapticUtils.SetHapticLevel(HapticUtils.HapticScale.Medium);
		private void DashPad_OnDashPadInteraction() => HapticUtils.SetHapticLevel(HapticUtils.HapticScale.Medium);
	}
}