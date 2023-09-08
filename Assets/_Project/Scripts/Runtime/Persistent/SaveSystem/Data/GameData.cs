using System;
using UnityEngine;

namespace Project.Persistent.SaveSystem
{
	[Serializable]
	public class GameData
	{
		public SerializableDictionary<int, Vector3> playerPositionDictionary;
		public SerializableDictionary<int, float> planePositionDictionary;
		public SerializableDictionary<int, string> activeCharacterTypeDictionary;
		public SerializableDictionary<int, SerializableDictionary<int, bool>> dictionaryOfcoinHolderDictionary;
		public SerializableDictionary<string, int> inventorySystemDictionary;
		public SerializableDictionary<int, SerializableDictionary<string, int>> dictionaryOfinventorySystemDictionary;

		// The values defined in this constructor will be the default values
		// the game starts with when there's no data to load
		public GameData()
		{
			playerPositionDictionary = new SerializableDictionary<int, Vector3>();
			planePositionDictionary = new SerializableDictionary<int, float>();
			activeCharacterTypeDictionary = new SerializableDictionary<int, string>();
			dictionaryOfcoinHolderDictionary = new SerializableDictionary<int, SerializableDictionary<int, bool>>();
			inventorySystemDictionary = new SerializableDictionary<string, int>();
			dictionaryOfinventorySystemDictionary = new SerializableDictionary<int, SerializableDictionary<string, int>>();
		}
	}
}