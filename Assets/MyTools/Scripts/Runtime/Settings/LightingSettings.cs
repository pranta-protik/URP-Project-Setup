using UnityEngine;

namespace MyTools
{
	[CreateAssetMenu(fileName = "LightingSettings", menuName = "ScriptableObjects/LightingSettings")]
	public class LightingSettings : ScriptableObject
	{
		public Material skyboxMat;
	}
}