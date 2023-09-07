using UnityEngine;

namespace MyTools.Settings
{
	[CreateAssetMenu(fileName = "LightingSettings", menuName = "ScriptableObjects/LightingSettings")]
	public class LightingSettingsSO : ScriptableObject
	{
		public Material skyboxMat;
	}
}