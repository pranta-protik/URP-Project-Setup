using System;
using UnityEngine;

namespace Project
{
	[Serializable]
	public class GameData
	{
		public SerializableDictionary<int, Vector3> playerPositionDictionary;
		public SerializableDictionary<int, SerializableDictionary<int, bool>> dictionaryOfcoinHolderDictionary;

		// The values defined in this constructor will be the default values
		// the game starts with when there's no data to load
		public GameData()
		{
			playerPositionDictionary = new SerializableDictionary<int, Vector3>();
			dictionaryOfcoinHolderDictionary = new SerializableDictionary<int, SerializableDictionary<int, bool>>();
		}
	}
}