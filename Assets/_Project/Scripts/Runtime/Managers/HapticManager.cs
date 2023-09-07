using DemoScene;
using MyTools.Utils;
using UnityEngine;

namespace Project.Managers
{
	public class HapticManager : MonoBehaviour
	{
		[SerializeField] private bool _enableLog = true;

		private void Awake() => HapticUtils.SetLogStatus(_enableLog);

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