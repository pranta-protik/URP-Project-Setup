using UnityEngine;
using UnityEngine.Rendering;

namespace MyTools.Settings
{
	[CreateAssetMenu(fileName = "LightingSettings", menuName = "ScriptableObjects/LightingSettings")]
	public class LightingSettingsSO : ScriptableObject
	{
		public Material skyboxMat;
		public AmbientMode ambientMode;
		[ColorUsage(true, true)] public Color ambientColor;
	}
}