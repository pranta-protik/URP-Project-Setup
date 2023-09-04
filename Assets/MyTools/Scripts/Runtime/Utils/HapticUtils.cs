using MoreMountains.NiceVibrations;
using UnityEngine;

namespace MyTools
{
	public static class HapticUtils
	{
		public enum HapticScale
		{
			Light = HapticTypes.LightImpact,
			Medium = HapticTypes.MediumImpact,
			Heavy = HapticTypes.HeavyImpact
		}

		private const string HAPTIC_STATUS = "HapticStatus";
		private static int _hapticStatus;
		private static bool _enableLog;

		static HapticUtils() => _hapticStatus = PlayerPrefs.GetInt(HAPTIC_STATUS, 1);

		public static void SetHapticLevel(HapticScale hapticScale)
		{
			if (_hapticStatus == 0) return;
			MMVibrationManager.Haptic((HapticTypes)hapticScale);

			if (_enableLog) DebugUtils.Log($"{hapticScale} Impact");
		}

		public static void SetHapticStatus(bool enable)
		{
			_hapticStatus = enable ? 1 : 0;
			PlayerPrefs.SetInt(HAPTIC_STATUS, _hapticStatus);
		}

		public static void SetLogStatus(bool enable)
		{
			_enableLog = enable;
		}
	}
}