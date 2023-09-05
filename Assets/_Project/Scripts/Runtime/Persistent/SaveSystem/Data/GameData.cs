using System;
using UnityEngine;

namespace Project
{
	[Serializable]
	public class GameData
	{
		public Vector3 playerPosition;

		// The values defined in this constructor will be the default values
		// the game starts with when there's no data to load
		public GameData()
		{
			playerPosition = Vector3.zero;
		}
	}
}