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
		public SerializableDictionary<int, SerializableDictionary<int, bool>> dictionaryOfCoinHolderDictionary;
		public SerializableDictionary<string, int> inventorySystemDictionary;
		public SerializableDictionary<int, SerializableDictionary<string, int>> dictionaryOfInventorySystemDictionary;
		public SerializableDictionary<string, int> planeHangerDictionary;

		// The values defined in this constructor will be the default values
		// the game starts with when there's no data to load
		public GameData()
		{
			playerPositionDictionary = new SerializableDictionary<int, Vector3>();
			planePositionDictionary = new SerializableDictionary<int, float>();
			activeCharacterTypeDictionary = new SerializableDictionary<int, string>();
			dictionaryOfCoinHolderDictionary = new SerializableDictionary<int, SerializableDictionary<int, bool>>();
			inventorySystemDictionary = new SerializableDictionary<string, int>();
			dictionaryOfInventorySystemDictionary = new SerializableDictionary<int, SerializableDictionary<string, int>>();
			planeHangerDictionary = new SerializableDictionary<string, int>();
		}
	}
}